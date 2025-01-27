using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;



public class DrawerInteractable : XRGrabInteractable
{
    [SerializeField] Transform DrawerTransform;
    [SerializeField] XRSocketInteractor KeySocket;
    [SerializeField] GameObject KeyIndicatorLight;
    [SerializeField] bool IsLocked;

    private Transform ParentTransform;
    private const string DefaultLayer = "Default";
    private const string GrabLayer = "Grab";
    private bool IsGrabbed;
    private Vector3 LimitPosition;
    [SerializeField] float DrawerLimitZ = .8f;
    [SerializeField] private Vector3 LimitDistance = new Vector3(.02f, .02f, 0f);

    [SerializeField] private TMP_Text DebugText;
    
    void Start()
    {
        if(KeySocket != null)
        {
            KeySocket.selectEntered.AddListener(OnDrawerUnlocked);
            KeySocket.selectExited.AddListener(OnDrawerLocked);
        }

        ParentTransform = transform.parent.transform;
        if(DrawerTransform != null)
        {
            LimitPosition = DrawerTransform.localPosition;
        }
        else
        {
            Debug.LogError("ERROR: null DrawerTransform");
        }

         _Mask = "Grab";
    }

    private void OnDrawerLocked(SelectExitEventArgs arg0)
    {
         IsLocked = true;
        Debug.Log("****Drawer Locked****");
    }

    private void OnDrawerUnlocked(SelectEnterEventArgs arg0)
    {
        IsLocked = false;
        if(KeyIndicatorLight != null)
        {
            KeyIndicatorLight.gameObject.SetActive(false);
        }

        Debug.Log("****Drawer Unlocked****");
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        //string s = "OnSelectEntered(): ";
        if(IsLocked == false)
        {
            //s += "IsLocked = false";
            // grab the object
            transform.SetParent(ParentTransform);
            IsGrabbed = true;
        }
        else
        {
          //  s += "IsLocked = true";
            //interactionLayers = InteractionLayerMask.GetMask(DefaultLayer);
            ChangeLayerMask(DefaultLayer);
        }
       // Debug.Log(s);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        //Debug.Log("OnSelectExited()");
        ChangeLayerMask(GrabLayer);
        //interactionLayers = InteractionLayerMask.GetMask(GrabLayer);
        IsGrabbed = false;
        transform.localPosition = DrawerTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsGrabbed == true && DrawerTransform != null)
        {
            DrawerTransform.localPosition = new Vector3(DrawerTransform.localPosition.x,
               DrawerTransform.localPosition.y, transform.localPosition.z);

            CheckLimits();
        }


        DebugText.text = "IsLocked: " + IsLocked + "\n";
        DebugText.text += "Mask: " + _Mask;

    }

    private void CheckLimits()
    {
        if(transform.localPosition.x >= LimitPosition.x + LimitDistance.x ||
            transform.localPosition.x <= LimitPosition.x - LimitDistance.x ||
            transform.localPosition.y >= LimitPosition.y + LimitDistance.y ||
            transform.localPosition.y <= LimitPosition.y - LimitDistance.y)
        {
            ChangeLayerMask(DefaultLayer);
        }
        else if(DrawerTransform.localPosition.z <= LimitPosition.z - LimitDistance.z)
        {
            IsGrabbed = false;
            DrawerTransform.localPosition = LimitPosition;
            ChangeLayerMask(DefaultLayer);
        }     
        else if(DrawerTransform.localPosition.z >= DrawerLimitZ + LimitDistance.z)
        {
            IsGrabbed = false;
            DrawerTransform.localPosition = new Vector3(DrawerTransform.localPosition.x,
                DrawerTransform.localPosition.y, DrawerLimitZ - .01f);
            ChangeLayerMask(DefaultLayer);
        }  
    }
    string _Mask;
    private void ChangeLayerMask(string mask)
    {
        Debug.Log("ChangeLayerMask(): " + mask);
        _Mask = mask;
        interactionLayers = InteractionLayerMask.GetMask(mask);
    }
}
