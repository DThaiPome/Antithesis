using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A means of using the witch's lightning.
/// </summary>
public abstract class ALightningSpell : MonoBehaviour
{
    /// <summary>
    /// Cast the lightning spell over a certain point.
    /// </summary>
    /// <param name="origin">the point for the lightning to spawn.</param>
    public abstract void Cast(Vector3 origin);

    /// <summary>
    /// Gets how much time is remaining on the cooldown.
    /// </summary>
    /// <returns>seconds remaining.</returns>
    public abstract float GetCooldownRemaining();

    /// <summary>
    /// Gets the full, constant cooldown time for each use of lightning.
    /// </summary>
    /// <returns>cooldown in seconds</returns>
    public abstract float GetMaxCooldown();
}
