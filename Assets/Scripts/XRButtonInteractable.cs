using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class XrButtonInteractable : XRSimpleInteractable
{
     // motodo - redo all the variables and clean up the stupid multi refereces for colors and use an emum
    [SerializeField] GameObject KeyIndicatorLight;
    [SerializeField] Image buttonImage;

    //[SerializeField] Color[] buttonColors = new Color[4];
    [SerializeField] private Color normalColor;
    [SerializeField] private Color highlightedColor;
    [SerializeField] private Color pressedColor;
    [SerializeField] private Color selectedColor;

    private bool isPressed;
    void Start()
    {
       // normalColor = buttonColors[0];
       // highlightedColor = buttonColors[1];
      //  pressedColor = buttonColors[2];
      //  selectedColor = buttonColors[3];
        ResetColor();
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        isPressed = false;
        buttonImage.color = highlightedColor;
    }
    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);
        if(!isPressed)
        {
            buttonImage.color = normalColor; 
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {        
        base.OnSelectEntered(args);
        isPressed = true;
        KeyIndicatorLight.SetActive(true);
        buttonImage.color = pressedColor;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        buttonImage.color = selectedColor;
    }
    
    public void ResetColor()
    {
        buttonImage.color = normalColor;
    }
}

