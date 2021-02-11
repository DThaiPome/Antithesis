using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AMonsterPlaceRangeDisplay : MonoBehaviour
{
    /// <summary>
    /// Set whether or not the display is active.
    /// </summary>
    /// <param name="active">"true" will make the display visible, "false" will hide it.</param>
    public abstract void SetActive(bool active);

    /// <summary>
    /// Sets the size of the display.
    /// </summary>
    /// <param name="radius">world units from the player.</param>
    public abstract void SetRadius(float radius);
}
