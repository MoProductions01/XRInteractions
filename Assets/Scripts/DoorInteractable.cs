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

    [SerializeField] Collider ClosedCollider;
    [SerializeField] Collider OpenCollider;

    [SerializeField] private bool IsClosed;
    [SerializeField] private Vector3 StartRotation;
    [SerializeField]private float StartAngleX;
    
    [SerializeField] private bool IsOpen;
    [SerializeField] private Vector3 EndRotation;
    [SerializeField] private float EndAngleX;

    protected override void Start()
    {
        base.Start();

        StartRotation = transform.localEulerAngles;
        StartAngleX = StartRotation.x;
        if (StartAngleX >= 180)
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

#if false
    bool HasPrinted = false;
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (DoorObject != null)
        {
            string s = "before: " + DoorObject.localEulerAngles.ToString("F3") + ", ";
            DoorObject.localEulerAngles = new Vector3(DoorObject.localEulerAngles.x,
                transform.localEulerAngles.y, DoorObject.localEulerAngles.z);
            s += "after: " + DoorObject.localEulerAngles.ToString("F3") + ", ";
            Debug.Log("s: " + s);
           /* if(HasPrinted == false) 
            {
                HasPrinted = true;
                Debug.Log("s: " + s);
            }*/
        }

        if (isSelected == true)
        {
            CheckLimits();
        }
    }
    #endif

    protected override void Update()
    {
        base.Update();
        if (DoorObject != null)
        {
            DoorObject.localEulerAngles = new Vector3(
                DoorObject.localEulerAngles.x,
                transform.localEulerAngles.y,
                DoorObject.localEulerAngles.z
            );
        }

        if (isSelected)
        {
            CheckLimits();
        }
    }

    private void CheckLimits()
    {   // if we're here then the interactable is selected and moving the hinge
        IsClosed = false;
        IsOpen = false;
        float localAngleX = transform.localEulerAngles.x;
        if (localAngleX >= 180f)
        {
            localAngleX -= 360f;
        }
        if (localAngleX >= StartAngleX + RotationLimits.x ||
           localAngleX <= StartAngleX - RotationLimits.x)
        {
            ReleaseHinge();

        }
    } //motodo - look into interfaces for CheckLimits(). Also check y/z angle?

    protected override void ResetHinge()
    {
        Debug.Log("ResetHinge name: " + this.name);        

        if (IsClosed == true)
        {
            transform.localEulerAngles = StartRotation;
        }
        else if(IsOpen == true)
        {
            Debug.Log("Reset transform to EndRotation");
            transform.localEulerAngles = EndRotation;
        }
        else
        {
            transform.localEulerAngles = new Vector3(StartAngleX,
                    transform.localEulerAngles.y, transform.localEulerAngles.z);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter name: " + this.name);
        if (other == ClosedCollider)
        {
            IsClosed = true;
            Debug.Log("Set IsClosed to true name: " + this.name);
            ReleaseHinge(); // this will eventually call ResetHinge()
        }
        else if(other == OpenCollider)
        {
            IsOpen = true;
            Debug.Log("Set IsOpen to true name: " + this.name);
            ReleaseHinge(); // this will eventually call ResetHinge()
        }
    }

    // string s = "Pre localAngleX: " + localAngleX + ", ";
    // s += "Post localAngleX: " + localAngleX;
    //Debug.Log(s);
}
