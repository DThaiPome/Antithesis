using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For testing kick behaviour in enemies - all enemies will be kicked.
/// </summary>
public class PlayerKickAlwaysHits : APlayerKick
{
    public PlayerKickAlwaysHits(float kickDistance) : base(kickDistance) { }

    public override bool WithinKick(GameObject self)
    {
        return true;
    }
}
