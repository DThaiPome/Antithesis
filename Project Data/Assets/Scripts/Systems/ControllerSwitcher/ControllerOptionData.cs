using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;

public class ControllerOptionData : AControllerOptionData
{
    private static ControllerOptionData instance;

    [SerializeField]
    private ControlType defaultControlType = ControlType.NonVRController;
    [SerializeField]
    private string controllerManagerTag = "PlayerControllerManager";

    private ControlType controlType;

    private bool xrEnabled;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else
        {
            Destroy(this);
            return;
        }

        this.controlType = this.defaultControlType;
        SceneManager.sceneLoaded += this.OnLevelLoaded;
    }

    void Start()
    {
        this.SetXREnabled(false);
        this.xrEnabled = false;
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        this.EnforceControlType();
    }

    public override void EnforceControlType()
    {
        switch(this.controlType)
        {
            case ControlType.VRController:
                this.SetXREnabled(true);
                break;
            case ControlType.NonVRController:
                this.SetXREnabled(false);
                break;
        }

        GameObject g = GameObject.FindGameObjectWithTag(this.controllerManagerTag);
        APlayerControllerManager playerControllerManager = g == null ? null : g.GetComponent<APlayerControllerManager>();
        if (playerControllerManager != null)
        {
            this.ChangeControlTypeWithManager(playerControllerManager);
        }
    }

    private void ChangeControlTypeWithManager(APlayerControllerManager manager)
    {
        switch(this.controlType)
        {
            case ControlType.VRController:
                manager.SwitchToVRController();
                break;
            case ControlType.NonVRController:
                manager.SwitchToNonVRController();
                break;
            default:
                return;
        }
    }

    public override void SetControlType(ControlType type)
    {
        this.controlType = type;
        this.EnforceControlType();
    }

    private void SetXREnabled(bool enabled)
    {
        if (enabled)
        {
            StartCoroutine(EnableXR());
        } else
        {
            XRGeneralSettings.Instance.Manager.StopSubsystems();
            XRGeneralSettings.Instance.Manager.DeinitializeLoader();
            this.xrEnabled = false;
        }
    }

    private IEnumerator EnableXR()
    {
        if (!this.xrEnabled)
        {
            yield return XRGeneralSettings.Instance.Manager.InitializeLoader();
            XRGeneralSettings.Instance.Manager.StartSubsystems();
        }
        this.xrEnabled = true;
    }

    public override bool IsXREnabled()
    {
        return this.xrEnabled;
    }
}
