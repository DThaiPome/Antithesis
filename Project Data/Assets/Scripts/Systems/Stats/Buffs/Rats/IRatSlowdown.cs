using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides a public-facing method to stop this buff - for the rat's slowdown.
/// </summary>
public interface IRatSlowdown : IStatBuff
{
    /// <summary>
    /// Ends this buff.
    /// </summary>
    void StopGrabbing();
}
