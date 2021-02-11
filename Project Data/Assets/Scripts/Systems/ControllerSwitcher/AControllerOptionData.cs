using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores information pertaining to what control type is being used (VR/nonVR), and sends this
/// information to the controller manager to switch to the right type.
/// </summary>
public abstract class AControllerOptionData : MonoBehaviour
{
    /// <summary>
    /// Specify which control type should be used.
    /// </summary>
    /// <param name="type">the control type to use.</param>
    public abstract void SetControlType(ControlType type);

    /// <summary>
    /// Actively set the current control type to whatever is set with SetControlType.
    /// </summary>
    public abstract void EnforceControlType();

    /// <summary>
    /// Check if XR is enabled.
    /// </summary>
    /// <returns>true if XR is enabled, false otherwise.</returns>
    public abstract bool IsXREnabled();
}
