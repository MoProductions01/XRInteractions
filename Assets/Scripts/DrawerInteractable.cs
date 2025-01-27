using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;



public class DrawerInteractable : XRGrabInteractable
{

    [SerializeField] XRSocketInteractor KeySocket;
    [SerializeField] bool IsLocked;

    private Transform ParentTransform;
    private const string DefaultLayer = "Default";
    private const string GrabLayer = "Grab";
    
    void Start()
    {
        if(KeySocket != null)
        {
            KeySocket.selectEntered.AddListener(OnDrawerUnlocked);
            KeySocket.selectExited.AddListener(OnDrawerLocked);
        }

        ParentTransform = transform.parent.transform;
    }

    private void OnDrawerLocked(SelectExitEventArgs arg0)
    {
         IsLocked = true;
        Debug.Log("****Drawer Locked****");
    }

    private void OnDrawerUnlocked(SelectEnterEventArgs arg0)
    {
        IsLocked = false;
        Debug.Log("****Drawer Unlocked****");
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        string s = "OnSelectEntered(): ";
        if(IsLocked == false)
        {
            s += "IsLocked = false";
            transform.SetParent(ParentTransform);
        }
        else
        {
            s += "IsLocked = true";
            interactionLayers = InteractionLayerMask.GetMask(DefaultLayer);
        }
        Debug.Log(s);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        Debug.Log("OnSelectExited()");
        interactionLayers = InteractionLayerMask.GetMask(GrabLayer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
