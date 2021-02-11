using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Management;

public class PlayerControllerManager : APlayerControllerManager
{
    [SerializeField]
    private GameObject vrRig;
    [SerializeField]
    private GameObject nonVrController;
    [SerializeField]
    private Camera witchCamera;
    [SerializeField]
    private Canvas witchUI;
    [SerializeField]
    private ADisplayManager displayManager;

    [SerializeField]
    private int vrDisplayID = 0;
    [SerializeField]
    private int nonVRDisplayID = 1;

    private bool xrEnabled;

    public override void SwitchToNonVRController()
    {
        this.SetVREnabled(false);
    }

    public override void SwitchToVRController()
    {
        this.SetVREnabled(true);
    }

    private void SetVREnabled(bool isVR)
    {
        this.vrRig.SetActive(isVR);
        this.nonVrController.SetActive(!isVR);
        
        int targetDisplay = isVR ? this.vrDisplayID : this.nonVRDisplayID;

        this.witchCamera.targetDisplay = targetDisplay;
        this.witchUI.targetDisplay = targetDisplay;

        this.displayManager.SetDisplays(isVR ? 1 : 2);
    }
}
