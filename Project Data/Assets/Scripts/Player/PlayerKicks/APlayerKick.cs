using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All kicks need a kick distance --
/// </summary>
public abstract class APlayerKick : IPlayerKick
{
    private float kickDistance;

    public APlayerKick(float kickDistance)
    {
        this.kickDistance = kickDistance;
    }

    public float GetKickDistance()
    {
        return this.kickDistance;
    }

    public abstract bool WithinKick(GameObject self);
}
