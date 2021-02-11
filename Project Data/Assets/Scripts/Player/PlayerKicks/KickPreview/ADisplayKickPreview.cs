using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Control the display of the player's kick range.
/// </summary>
public abstract class ADisplayKickPreview : MonoBehaviour
{
    /// <summary>
    /// Enable or disable the kick preview.
    /// </summary>
    /// <param name="enabled">true to enable the display, false to disable the display.</param>
    public abstract void SetDisplayEnabled(bool enabled);

    /// <summary>
    /// Set the dimensions of the kick display.
    /// </summary>
    /// <param name="distance">the distance range of the kick.</param>
    /// <param name="angle">the angle range of the kick.</param>
    public abstract void SetKickRange(float distance, float angle);
}
