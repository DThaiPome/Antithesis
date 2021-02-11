using System.Security.Principal;
using UnityEngine;

// Storage for the stats of the witch
public abstract class AWitchStatBlock : MonoBehaviour
{
    /// <summary>
    /// Get the current Mana level
    /// </summary>
    /// <returns>the current Mana</returns>
    public abstract int GetMana();

    /// <summary>
    /// Modifies the Mana by the given amount. May not change the value beyond certain limits
    /// </summary>
    /// <param name="dM">the change in Mana</param>
    /// <returns>the new Mana value</returns>
    public abstract int ModifyMana(int dM);
}
