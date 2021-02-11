using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;

public class ControllerUIListener : AControllerUIListener
{
    [SerializeField]
    private string gameSceneName = "Environment";
    [SerializeField]
    private AControllerOptionData controllerData;

    public override void SetNonVRControls()
    {
        this.PSetNonVRControls();
    }

    public override void SetVRControls()
    {
        this.PSetVRControls();
    }

    public override void StartGame()
    {
        StartCoroutine(this.PStartGame(false));
    }

    public override void StartNonVRGame()
    {
        this.PSetNonVRControls();
        StartCoroutine(this.PStartGame(false));
    }

    public override void StartVRGame()
    {
        this.PSetVRControls();
        StartCoroutine(this.PStartGame(true));
    }

    private void PSetNonVRControls()
    {
        this.controllerData.SetControlType(ControlType.NonVRController);
    }

    private void PSetVRControls()
    {
        this.controllerData.SetControlType(ControlType.VRController);
    }

    private IEnumerator PStartGame(bool vrEnabled)
    {
        while (this.controllerData.IsXREnabled() != vrEnabled)
        {
            yield return null;
        }
        SceneManager.LoadScene(this.gameSceneName);
    }
}
