using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ADisplayManager : MonoBehaviour
{
    /// <summary>
    /// Activate this many displays.
    /// </summary>
    /// <param name="count">the new number of active displays (can never be decreased)</param>
    public abstract void SetDisplays(int count);
}
