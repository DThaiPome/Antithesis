using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An object to disable and enable the objects necessary to use a particular player controller (VR/nonVR)
/// </summary>
public abstract class APlayerControllerManager : MonoBehaviour
{
    /// <summary>
    /// Disable the nonVR controller, enable the VR controller.
    /// Deactivate the second display.
    /// Set the witch camera and UI to output to display 1.
    /// </summary>
    public abstract void SwitchToVRController();

    /// <summary>
    /// Enable the nonVR controller, disable the VR controller.
    /// Activate the second display.
    /// Set the witch camera and UI to output to display 2.
    /// </summary>
    public abstract void SwitchToNonVRController();
}
