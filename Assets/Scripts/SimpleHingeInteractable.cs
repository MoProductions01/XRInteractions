using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SimpleHingeInteractable : XRSimpleInteractable
{
    private Transform GrabHand;

    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(GrabHand != null)
        {
            transform.LookAt(GrabHand.transform);
          //  Debug.Log("looking at grab hand");
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);    
        GrabHand = args.interactorObject.transform;
       // Debug.Log("GrabHand: " + GrabHand.name);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {        
        base.OnSelectExited(args);
        GrabHand = null;
     //   Debug.Log("GrabHand is null");
    }
}
