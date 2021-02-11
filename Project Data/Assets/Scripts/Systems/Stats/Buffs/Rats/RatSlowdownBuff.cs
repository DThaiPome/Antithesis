using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The buff used to slow the player down when a rat is grabbing them.
/// </summary>
public class RatSlowdownBuff : AStatBuff, IRatSlowdown
{
    private float speedMultiplier;
    private bool stopped;

    /// <summary>
    /// Creates a new buff that changes the player's speed by a certain multiplier.
    /// </summary>
    /// <param name="speedMultiplier">the multipler to change the speed by.</param>
    public RatSlowdownBuff(float speedMultiplier)
    {
        this.speedMultiplier = speedMultiplier;
        this.stopped = false;
    }

    public override int GetDex(int dex)
    {
        return (int)(base.GetDex(dex) * this.speedMultiplier);
    }

    /// <summary>
    /// Cancels this buff if it is stopped.
    /// </summary>
    /// <param name="deltaTime">the change in time.</param>
    /// <returns>this buff unchanged if not stopped, null otherwise.</returns>
    public override IStatBuff Update(float deltaTime)
    {
        IStatBuff buff = base.Update(deltaTime);
        return stopped ? null : buff;
    }

    public void StopGrabbing()
    {
        this.stopped = true;
    }
}
