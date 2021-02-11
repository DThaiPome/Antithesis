using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;

/// <summary>
/// Data operations for a non-VR (non-witch) player.
/// </summary>
public class FirstPersonPlayerObject : APlayerObject
{
    [SerializeField]
    public static KeyCode kickInput;
    [SerializeField]
    private AStatBlock playerStatBlock;
    /// <summary>
    /// Put an empty object in front of the FPS camera, as a child of
    /// it, so that it moves with the camera's rotations.
    /// </summary>
    [SerializeField]
    private Transform shakeSpeedChecker;

    private IObjectSpeedData shakeSpeedData;

    void Awake()
    {
        //This will happen in APlayerControllerManager
        //XRGeneralSettings.Instance.Manager.DeinitializeLoader();
    }

    protected override void AfterStart()
    {
        base.AfterStart();
        this.shakeSpeedData = new ObjectSpeedDataCollector(shakeSpeedChecker, false);
    }

    void FixedUpdate()
    {
        this.shakeSpeedData.SampleData(Time.fixedDeltaTime);
    }

    protected override void AddBuffToPlayer(IStatBuff buff)
    {
        this.playerStatBlock.AddBuff(buff);
    }

    protected override void DamagePlayer(int damage)
    {
        this.playerStatBlock.ModifyHP(-damage);
        Debug.Log("Player damage taken: " + damage);
    }

    protected override float GetShakeInput()
    {
        return this.shakeSpeedData.GetVelocity().magnitude;
    }

    protected override bool GetKickInput()
    {
        return Input.GetKeyDown(kickInput);
    }

    protected override IPlayerKick GetKickData()
    {
        return new PlayerConeKick(
                   this.kickDistance,
                   this.transform,
                   this.kickHeightOffset,
                   this.kickAngleRange,
                   this.kickRange); ;
    }
}
