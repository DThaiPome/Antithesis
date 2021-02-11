using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A place to detail the specifications of what the player object should be doing
/// (i.e stat block operations, etc.)
/// </summary>
public abstract class APlayerObject : MonoBehaviour
{
    [SerializeField]
    protected float kickDistance = 2;
    [SerializeField]
    protected float kickRange = 1.5f;
    [SerializeField]
    protected float kickAngleRange = 75;
    [SerializeField]
    protected float kickHeightOffset = 1;
    [SerializeField]
    protected float kickCooldown = 3;

    protected float kickCooldownRemaining;

    void Start()
    {
        EventChannel.current.onDamagePlayerEvent += this.DamagePlayer;
        EventChannel.current.onAddBuffToPlayerEvent += this.AddBuffToPlayer;

        this.AfterStart();
    }

    /// <summary>
    /// Add more functionality at the start of the scene.
    /// </summary>
    protected virtual void AfterStart() { }

    /// <summary>
    /// Behaviour for when the player receives damage.
    /// </summary>
    /// <param name="damage">the amount of damage received.</param>
    protected abstract void DamagePlayer(int damage);

    /// <summary>
    /// Behaviour for when the player receives a stat buff.
    /// </summary>
    /// <param name="buff">the stat buff received.</param>
    protected abstract void AddBuffToPlayer(IStatBuff buff);

    void Update()
    {
        float shakeInput = this.GetShakeInput();
        this.SendShakeInput(shakeInput);
        this.DoKick();

        this.AfterUpdate();
    }

    private void DoKick()
    {
        if (this.CanKick() && this.GetKickInput())
        {
            this.kickCooldownRemaining = this.kickCooldown;
            IPlayerKick kickData = this.GetKickData();
            EventChannel.current.onPlayerKick(kickData);
        }
        this.kickCooldownRemaining = Mathf.Max(this.kickCooldownRemaining - Time.deltaTime, 0);
    }

    /// <summary>
    /// Add more functionality after every frame.
    /// </summary>
    protected virtual void AfterUpdate() { }

    //Do whatever needs to be done externally with the shake input.
    private void SendShakeInput(float shakeInput)
    {
        RatBehaviour.onShakenOff(shakeInput);
    }

    /// <summary>
    /// Returns the speed of the player's shake input (i.e for warding
    /// off rats).
    /// </summary>
    /// <returns>the shake input.</returns>
    protected abstract float GetShakeInput();

    /// <summary>
    /// Returns whether or not the kick input has been activated this frame.
    /// </summary>
    /// <returns>true, if the player is kicking this frame.</returns>
    protected abstract bool GetKickInput();

    /// <summary>
    /// Retrieves the kick data needed to perform a kick.
    /// </summary>
    /// <returns>kick data.</returns>
    protected abstract IPlayerKick GetKickData();

    protected bool CanKick()
    {
        return this.kickCooldownRemaining == 0;
    }
}
