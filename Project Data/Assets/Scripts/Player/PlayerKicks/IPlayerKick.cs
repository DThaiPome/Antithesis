using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents an object that holds data from a player's kick.
/// An enemy can check if it is within the range of a kick.
/// </summary>
public interface IPlayerKick
{
    /// <summary>
    /// Check if this object would be affected by the kick, under normal circumstances.
    /// (Effectively checking if the object is within a "hitbox")
    /// </summary>
    /// <param name="self">the game object to check.</param>
    /// <returns>true if the game object would be kicked, false otherwise.</returns>
    bool WithinKick(GameObject self);

    /// <summary>
    /// Get the base distance of the push of a kick.
    /// </summary>
    /// <returns>distance in units.</returns>
    float GetKickDistance();
}
