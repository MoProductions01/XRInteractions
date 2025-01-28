using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using System;

public class SimpleUIControl : MonoBehaviour
{
    [SerializeField] XrButtonInteractable StartButton;
    [SerializeField] string[] MessageStrings;
    [SerializeField] TMP_Text[] MessageTexts;
    [SerializeField] GameObject KeyIndicatorLight;

    public void SetText(string msg)
    {
        for(int i = 0; i < MessageTexts.Length; i++)
        {
            MessageTexts[i].text = msg;
        }
    }
    
    void Start()
    {
        if(StartButton != null)
        {
            StartButton.selectEntered.AddListener(StartButtonPressed);
        }
    }

    private void StartButtonPressed(SelectEnterEventArgs arg0)
    {
        SetText(MessageStrings[1]);
        if(KeyIndicatorLight != null)
        {
            KeyIndicatorLight.SetActive(true);
        }
    }
}
