using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public abstract class SimpleHingeInteractable : XRSimpleInteractable
{   // motodo - make sure only the necessary things are serialized by the end
    [SerializeField] Vector3 PositionLimits;
    [SerializeField] private Transform GrabHand;
    private Collider HingeCollider;
    private Vector3 HingePosition;
    [SerializeField] bool IsLocked;
    private const string Default_Layer = "Default"; // monote - add "_" to const
    private const string Grab_Layer = "Grab"; // monote - see if we can share this with DrawerInteractable

    protected virtual void Start()
    {
        HingeCollider = GetComponent<Collider>();
    }

    public void UnlockHinge()
    {
        // Debug.Log("SimpleHingeInteractable.UnlockHinge()");
        IsLocked = false;
    }
    public void LockHinge()
    {
        //  Debug.Log("SimpleHingeInteractable.LockHinge()");
        IsLocked = true;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (GrabHand != null)
        {
            TrackHand();
            //  Debug.Log("looking at grab hand");
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (IsLocked == false)
        {
            base.OnSelectEntered(args);
            GrabHand = args.interactorObject.transform;
            float localAngleY = GrabHand.transform.localEulerAngles.y;
            if (localAngleY >= 180f)
            {
                localAngleY -= 360f;
            } // motodo - there's an issue with the door popping when grabbed due ot it's rotation adjusting with the grab item
            Debug.Log("Grabbed with: " + GrabHand.gameObject.name + ", localAngleY: " + localAngleY.ToString("F3"));
            // Debug.Log("GrabHand: " + GrabHand.name);
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        Debug.Log("SHI.OnSelectExited name: " + this.name);
        base.OnSelectExited(args);
        GrabHand = null;
        ChangeLayerMask(Grab_Layer);
        ResetHinge();
        //   Debug.Log("G
    }

    private void TrackHand()
    {   // motodo maybe update the >=,<= to a more Dist() thing
        //transform.LookAt(GrabHand.transform);
        transform.LookAt(GrabHand, transform.forward);
        HingePosition = HingeCollider.bounds.center;
        if (GrabHand.position.x >= HingePosition.x + PositionLimits.x ||
            GrabHand.position.x <= HingePosition.x - PositionLimits.x)
        {
            ReleaseHinge();
           // Debug.Log("TrackHand() release hinge X");
        }
        else if (GrabHand.position.y >= HingePosition.y + PositionLimits.y ||
            GrabHand.position.y <= HingePosition.y - PositionLimits.y)
        {
            ReleaseHinge();
            //Debug.Log("TrackHand() release hinge Y");
        }
        else if (GrabHand.position.z >= HingePosition.z + PositionLimits.z ||
            GrabHand.position.z <= HingePosition.z - PositionLimits.z)
        {
            ReleaseHinge();
            //Debug.Log("TrackHand() release hinge Z");
        }
    }

    public void ReleaseHinge()
    {
        ChangeLayerMask(Default_Layer); // this gives us OnSelectExited
        if(GrabHand == null)
        {
            Debug.Log("Fail safe activated");
            ResetHinge();
            ChangeLayerMask(Grab_Layer);
        }
    }

    protected abstract void ResetHinge();

    private void ChangeLayerMask(string mask)
    {
        // Debug.Log("SimpleHingeInteractable.ChangeLayerMask(): " + mask);
        interactionLayers = InteractionLayerMask.GetMask(mask);
    }




}
