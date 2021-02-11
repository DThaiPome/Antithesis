using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

//Storage for the stats of an entity (HP, armor, etc.)
public abstract class AStatBlock : MonoBehaviour
{
    /// <summary>
    /// Get the current HP
    /// </summary>
    /// <returns>the current HP</returns>
    public abstract int GetHP();
    /// <summary>
    /// Get the current dexterity
    /// </summary>
    /// <returns>the current dexterity</returns>
    public abstract int GetDex();

    /// <summary>
    /// Returns the speed that corresponds to this dexterity score
    /// </summary>
    /// <returns>speed in units/sec</returns>
    public static float DexToSpeed(int dexScore)
    {
        return (float)dexScore / 4.0f;
    }

    /// <summary>
    /// Get the current strength
    /// </summary>
    /// <returns>the current strength</returns>
    public abstract int GetStr();

    /// <summary>
    /// Modifies the HP by the given amount. May not change the value beyond certain limits
    /// </summary>
    /// <param name="dHP">the change in HP</param>
    /// <returns>the new HP value</returns>
    public abstract int ModifyHP(int dHP);

    /// <summary>
    /// Adds the given buff to this stat block. It will affect the score values retrieved
    /// </summary>
    /// <param name="buff">the buff to add</param>
    public abstract void AddBuff(IStatBuff buff);

    void Awake()
    {
        this.AfterAwake();
    }

    protected virtual void AfterAwake() { }

    void Start()
    {
        this.AfterStart();
    }

    protected virtual void AfterStart() { }

    void Update()
    {
        this.AfterUpdate();
    }

    protected virtual void AfterUpdate() { }

    void OnDestroy()
    {
        this.AfterDestroy();
    }

    protected virtual void AfterDestroy() { }
}  
