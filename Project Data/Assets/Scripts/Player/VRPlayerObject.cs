using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayerObject : APlayerObject
{
    [SerializeField]
    private string kickInput;
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private AStatBlock playerStatBlock;
    [SerializeField]
    private Transform leftHand;
    [SerializeField]
    private Transform rightHand;
    [SerializeField]
    private ADisplayKickPreview kickPreview;

    private IObjectSpeedData leftHandSpeedData;
    private IObjectSpeedData rightHandSpeedData;

    void Awake()
    {
        //this.camera.stereoTargetEye = StereoTargetEyeMask.Both;
    }

    protected override void AfterStart()
    {
        base.AfterStart();
        this.leftHandSpeedData = new ObjectSpeedDataCollector(leftHand);
        this.rightHandSpeedData = new ObjectSpeedDataCollector(rightHand);
    }

    protected override void AfterUpdate()
    {
        base.AfterUpdate();
        this.UpdateKickPreview();
    }

    private void UpdateKickPreview()
    {
        this.kickPreview.SetKickRange(this.kickRange, this.kickAngleRange);
        this.kickPreview.SetDisplayEnabled(this.CanKick());
    }

    void FixedUpdate()
    {
        this.UpdateSpeedCollectors(Time.fixedDeltaTime);
    }

    private void UpdateSpeedCollectors(float deltaTime)
    {
        this.leftHandSpeedData.SampleData(deltaTime);
        this.rightHandSpeedData.SampleData(deltaTime);
    }

    protected override void DamagePlayer(int damage)
    {
        this.playerStatBlock.ModifyHP(-damage);
        Debug.Log("Player damage taken: " + damage);
    }

    protected override void AddBuffToPlayer(IStatBuff buff)
    {
        this.playerStatBlock.AddBuff(buff);
    }

    protected override float GetShakeInput()
    {
        return Mathf.Max(
            this.leftHandSpeedData.GetVelocity().magnitude,
            this.rightHandSpeedData.GetVelocity().magnitude);
    }

    protected override bool GetKickInput()
    {
        return Input.GetButtonDown(this.kickInput);
    }

    protected override IPlayerKick GetKickData()
    {
       return new PlayerConeKick(
                   this.kickDistance,
                   this.camera.transform,
                   this.kickHeightOffset,
                   this.kickAngleRange,
                   this.kickRange);
    }
}
