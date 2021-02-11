using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NonVRInteractor : XRBaseInteractor
{
    private XRGrabInteractable objectToInteractWith;

    public NonVRInteractor(XRGrabInteractable objectToInteractWith)
    {
        this.objectToInteractWith = objectToInteractWith;
    }

    public override void GetValidTargets(List<XRBaseInteractable> validTargets)
    {
        validTargets.Add(objectToInteractWith);
    }
}
