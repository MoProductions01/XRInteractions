using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : SimpleHingeInteractable
{
    
    [SerializeField] Transform DoorObject;

    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if(DoorObject != null)
        {
            DoorObject.localEulerAngles = new Vector3(DoorObject.localEulerAngles.x,
                transform.localEulerAngles.y, DoorObject.localEulerAngles.z);
        }
    }
}
