using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorInteractable : SimpleHingeInteractable
{
    [SerializeField] ComboLock ComboLock;
    [SerializeField] Transform DoorObject;
    [SerializeField] Vector3 RotationLimits;
    private Transform StartRotation; // monote - rename to StartTransform
    private float StartAngleX;

    void Start()
    {
        StartRotation = transform;
        StartAngleX = StartRotation.localEulerAngles.x;
        if(StartAngleX >= 180)
        {
            StartAngleX -= 360;
        }

        if (ComboLock != null)
        {
            ComboLock.UnlockAction += OnUnlocked;
            ComboLock.LockAction += OnLocked;
            // Debug.Log("Have ComboLock: " + ComboLock.name);
        }
        else
        {
            // Debug.LogError("ERROR: No ComboLock in DoorInteractable.cs");
        }
    }

    private void OnUnlocked()
    {
        //   Debug.Log("DoorInteractable.OnUnlocked()");
        UnlockHinge();
    }

    private void OnLocked()
    {
        // Debug.Log("DoorInteractable.OnLocked()");
        LockHinge();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (DoorObject != null)
        {
            DoorObject.localEulerAngles = new Vector3(DoorObject.localEulerAngles.x,
                transform.localEulerAngles.y, DoorObject.localEulerAngles.z);
        }

        if(isSelected == true)
        {
            CheckLimits();
        }
    }
    
    private void CheckLimits()
    {
        float localAngleX = transform.localEulerAngles.x;
        if (localAngleX >= 180f)
        {
            localAngleX -= 360f;
        }
        if(localAngleX >= StartAngleX + RotationLimits.x ||
           localAngleX <= StartAngleX - RotationLimits.x)
        {
            ReleaseHinge();
            transform.localEulerAngles = new Vector3(StartAngleX, 
                transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
    } //motodo - look into interfaces for CheckLimits()

    // string s = "Pre localAngleX: " + localAngleX + ", ";
    // s += "Post localAngleX: " + localAngleX;
    //Debug.Log(s);
}
