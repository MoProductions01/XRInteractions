using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class ComboLock : MonoBehaviour
{
    public UnityAction UnlockAction;  
    public UnityAction LockAction;  
    private void OnUnlocked() => UnlockAction?.Invoke();  
    private void OnLocked() => LockAction?.Invoke();

    [SerializeField] TMP_Text UserInputText;
    [SerializeField] XrButtonInteractable[] ComboButtons;
    [SerializeField] TMP_Text InfoText;
    private const string Start_String = "Enter 3 Digit Combo";
    private const string Reset_String = "Enter 3 Digits To Reset Combo";
    [SerializeField] Image LockedPanel;
    [SerializeField] Color UnlockedColor;
    [SerializeField] Color LockedColor;
    [SerializeField] TMP_Text LockedText;
    private const string Unlocked_String = "Unlocked";
    private const string Locked_String = "Locked";
    [SerializeField] bool IsLocked;
    [SerializeField] bool IsResettable;
    private bool ShouldResetCombo;
    [SerializeField] int[] ComboValues = new int[3];
    [SerializeField] int[] InputValues;
    private int MaxButtonPresses;
    private int NumButtonPresses;

    void Start()
    {
        MaxButtonPresses = ComboValues.Length;
        ResetUserValues();
        // InputValues = new int[MaxButtonPresses];
        // UserInputText.text = "";

        for (int i = 0; i < ComboButtons.Length; i++)
        {
            ComboButtons[i].selectEntered.AddListener(OnComboButtonPressed);
        }
    }

    private void OnComboButtonPressed(SelectEnterEventArgs arg0)
    {   // motodo - change this bullshit to finding the button from a list or something
        if (NumButtonPresses >= MaxButtonPresses)
        {
            //  too many button presses            
        }
        else
        {
            for (int i = 0; i < ComboButtons.Length; i++)
            {
                if (arg0.interactableObject.transform.name == ComboButtons[i].transform.name)
                {
                    UserInputText.text += i.ToString();
                    InputValues[NumButtonPresses] = i;
                }
                else
                {
                    ComboButtons[i].ResetColor();
                }
            }
            NumButtonPresses++;
            if (NumButtonPresses == MaxButtonPresses)
            {
                // check combo
                CheckCombo();
            }
        }
    }

    private void CheckCombo()
    {
        if (ShouldResetCombo == true)
        {
            ShouldResetCombo = false;
            LockCombo();
            return;
        }
        int matches = 0;

        for (int i = 0; i < MaxButtonPresses; i++)
        {   // motodo don't do it this way look for a not equal value and bail
            if (InputValues[i] == ComboValues[i])
            {
                matches++;
            }
        }
        if (matches == MaxButtonPresses)
        {
            UnlockCombo();
        }
        else
        {
            ResetUserValues();
        }
    }

    
    private void UnlockCombo()
    {
       // Debug.Log("ComboLock.UnlockCombo()");
        IsLocked = false;
        OnUnlocked();
        LockedPanel.color = UnlockedColor;
        LockedText.text = Unlocked_String;
        if (IsResettable == true)
        {
            ResetCombo();
        }
    }

    private void LockCombo()
    {
       // Debug.Log("ComboLock.LockCombo(");        
        IsLocked = true;
        OnLocked();        
        LockedPanel.color = LockedColor;
        LockedText.text = Locked_String;
        InfoText.text = Start_String;
        for(int i=0; i<MaxButtonPresses; i++)
        {
            ComboValues[i] = InputValues[i];
        }
        ResetUserValues();
    }

    private void ResetCombo()
    {
        InfoText.text = Reset_String;
        ResetUserValues();
        ShouldResetCombo = true;
    }

    private void ResetUserValues()
    {
        InputValues = new int[MaxButtonPresses];
        UserInputText.text = "";
        NumButtonPresses = 0;
    }
}