using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Applies a buff to a stat block, that modifies the values in some non-permanent way.
public interface IStatBuff
{
    /// <summary>
    /// Get the current HP, including this buff's effects
    /// </summary>
    /// <param name="hp">the hp to buff</param>
    /// <returns>the current HP</returns>
    int GetHP(int hp);
    /// <summary>
    /// Get the current dexterity, including this buff's effects
    /// </summary>
    /// <param name="dex">the dex to buff</param>
    /// <returns>the current dexterity</returns>
    int GetDex(int dex);
    /// <summary>
    /// Get the current strength, including this buff's effects
    /// </summary>
    /// <param name="str">the str to buff</param>
    /// <returns>the current strength</returns>
    int GetStr(int str);

    /// <summary>
    /// Updates the buff by one frame (useful for time-based buffs?)
    /// </summary>
    /// <returns>the new stat buff (still mutates)</returns>
    IStatBuff Update(float deltaTime);

    
}
