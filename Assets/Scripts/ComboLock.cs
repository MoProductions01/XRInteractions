using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class ComboLock : MonoBehaviour
{
    [SerializeField] TMP_Text UserInputText;
    [SerializeField] XrButtonInteractable[] ComboButtons;   

    void Start()
    {
        UserInputText.text = "";
        for(int i=0; i<ComboButtons.Length; i++)
        {
            ComboButtons[i].selectEntered.AddListener(OnComboButtonPresseD);
        }
    }

    private void OnComboButtonPresseD(SelectEnterEventArgs arg0)
    {   // motodo - change this bullshit to finding the button from a list or something
        for(int i = 0; i < ComboButtons.Length; i++)
        {
            if(arg0.interactableObject.transform.name == ComboButtons[i].transform.name)
            {
                UserInputText.text = i.ToString();
            }
            else
            {
                ComboButtons[i].ResetColor();
            }
        }
    }
    
}