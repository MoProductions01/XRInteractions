using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.UI;

public class ComboLock : MonoBehaviour
{
    [SerializeField] TMP_Text UserInputText;
    [SerializeField] XrButtonInteractable[] ComboButtons;
    [SerializeField] TMP_Text InfoText;
    private const string StartString = "Enter 3 Digit Combo";
    private const string ResetString = "Enter 3 Digits To Reset Combo";
    [SerializeField] Image LockedPanel;
    [SerializeField] Color UnlockedColor;
    [SerializeField] Color LockedColor;
    [SerializeField] TMP_Text LockedText;
    private const string UnlockedString = "Unlocked";
    private const string LockedString = "Locked";
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
        IsLocked = false;
        LockedPanel.color = UnlockedColor;
        LockedText.text = UnlockedString;
        if (IsResettable == true)
        {
            ResetCombo();
        }
    }

    private void LockCombo()
    {
        IsLocked = true;
        LockedPanel.color = LockedColor;
        LockedText.text = LockedString;
        InfoText.text = StartString;
        for(int i=0; i<MaxButtonPresses; i++)
        {
            ComboValues[i] = InputValues[i];
        }
        ResetUserValues();
    }

    private void ResetCombo()
    {
        InfoText.text = ResetString;
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