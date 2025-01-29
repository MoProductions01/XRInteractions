using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SimpleHingeInteractable : XRSimpleInteractable
{
    private Transform GrabHand;
    [SerializeField] bool IsLocked;
    private const string Default_Layer = "Default"; // monote - add "_" to const
    private const string Grab_Layer = "Grab"; // monote - see if we can share this with DrawerInteractable

    void Start()
    {

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
            transform.LookAt(GrabHand.transform);
            //  Debug.Log("looking at grab hand");
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (IsLocked == false)
        {
            base.OnSelectEntered(args);
            GrabHand = args.interactorObject.transform;
            // Debug.Log("GrabHand: " + GrabHand.name);
        }

    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        GrabHand = null;
        ChangeLayerMask(Grab_Layer);
        //   Debug.Log("G
    }

    public void ReleaseHinge()
    {
        ChangeLayerMask(Default_Layer);
    }

    private void ChangeLayerMask(string mask)
    {
        Debug.Log("SimpleHingeInteractable.ChangeLayerMask(): " + mask);
        interactionLayers = InteractionLayerMask.GetMask(mask);
    }




}
