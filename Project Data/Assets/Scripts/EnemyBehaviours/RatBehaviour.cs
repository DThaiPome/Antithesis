using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behaviour for rat enemies.
/// </summary>
public class RatBehaviour : AEnemyBehaviour
{
    [SerializeField]
    private float distanceToLatch = 0.25f;
    [SerializeField]
    private float slowdownFactor = 0.5f;
    [SerializeField]
    private float distanceOnUnlatch = 3;
    [SerializeField]
    private float distanceFromPlayerWhileGrabbing = 1;
    [SerializeField]
    private float shakeOffThreshold = 3;
    [SerializeField]
    private float minSpeedToShakeOff = 1;
    [SerializeField]
    private float kickWeightForShakeOff = .1f;

    private RatState currentState;
    private IRatSlowdown slowdownBuff;

    private Transform initParent;

    private bool shakenOff;
    private float shakeOffThresholdSoFar;
    public static event Action<float> onShakenOffEvent;

    private bool collisionBearTrap;
    private int countBear = 0;
    private int timeBear = 10;

    public static void onShakenOff(float input)
    {
        if (onShakenOffEvent != null)
        {
            onShakenOffEvent(input);
        }
    }

    /// <summary>
    /// State machine for rat
    /// Chasing - running after player
    /// Grabbing - attached to player, slowing them down
    /// </summary>
    private enum RatState
    {
        Chasing, Grabbing
    }

    // Target destination is at the player unless grabbing.
    protected override Vector3 GetDestination()
    {
        switch(this.currentState)
        {
            case RatState.Chasing:
                return this.playerTransform.position;
            case RatState.Grabbing:
                return this.transform.position;
            default:
                return this.transform.position;
        }
    }
    
    // Rat speed is 2 unless grabbing.
    protected override float GetMoveSpeed()
    {
         switch (this.currentState)
        {
                case RatState.Chasing:
                    return 2;
                case RatState.Grabbing:
                    return 0;
                default:
                    return 0;
        }
    }

    protected override void AfterStart()
    {
        base.AfterStart();
        this.InitRat();
    }

    private void InitRat()
    {
        this.currentState = RatState.Chasing;
        this.InitBuff();
        this.initParent = this.transform.parent;
        this.shakenOff = false;
        this.shakeOffThresholdSoFar = 0;

        RatBehaviour.onShakenOffEvent += this.OnShakenOff;
    }

    protected override void AfterDestroy()
    {
        base.AfterDestroy();
        RatBehaviour.onShakenOffEvent -= this.OnShakenOff;
    }

    private void InitBuff()
    {
        this.slowdownBuff = new RatSlowdownBuff(this.slowdownFactor);
    }

    protected override void AfterUpdate()
    {
        base.AfterUpdate();
        this.UpdateRatState();
    }

    /// <summary>
    /// Behaviour per-frame for each state
    /// </summary>
    private void UpdateRatState()
    {
        switch(this.currentState)
        {
            case RatState.Chasing:
                this.ChasingUpdate();
                break;
            case RatState.Grabbing:
                this.GrabbingUpdate();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// If close enough to the player, latch onto them and start grabbing.
    /// </summary>
    private void ChasingUpdate()
    {
        this.LookAtPlayerLaterally();

        Vector3 lateralPlayerPosition = new Vector3(this.playerTransform.position.x, this.transform.position.y, this.playerTransform.position.z);
        if (Vector3.Distance(this.transform.position, lateralPlayerPosition) < this.distanceToLatch)
        {
            this.StartGrabbing();
        }
    }

    /// <summary>
    /// Create the slowdown buff and switch states.
    /// </summary>
    private void StartGrabbing()
    {
        this.currentState = RatState.Grabbing;
        this.transform.position = this.GetGrabPoint();
        this.transform.SetParent(this.playerTransform);
        EventChannel.current.onAddBuffToPlayer(this.slowdownBuff);
    }

    private Vector3 GetGrabPoint()
    {
        Vector3 playerPos = new Vector3(this.playerTransform.position.x, this.transform.position.y, this.playerTransform.position.z);
        float moveDistance = Vector3.Distance(this.transform.position, playerPos) - this.distanceFromPlayerWhileGrabbing;
        Vector3 toPlayer = (this.transform.position - playerTransform.position).normalized;
        Vector3 grabPos = this.transform.position + (toPlayer * moveDistance);
        return grabPos;
    }

    /// <summary>
    /// Stop grabbing .. eventually
    /// </summary>
    private void GrabbingUpdate()
    {
        this.LookUp();
        if (!StillGrabbing())
        {
            this.StopGrabbing();
        }
    }

    private void LookUp()
    {
        this.LookAtPlayerLaterally();
        Quaternion tiltUp = Quaternion.AngleAxis(90, Vector3.right);
        this.transform.rotation *= tiltUp;
    }

    // Revert the player's speed to normal and die.
    private void StopGrabbing()
    {
        this.slowdownBuff.StopGrabbing();
        this.InitBuff();
        this.transform.parent = this.initParent;
        this.PushAwayFromPlayer();
        this.shakenOff = false;
        this.shakeOffThresholdSoFar = 0;
        this.currentState = RatState.Chasing;

        this.Die();
    }

    // Used for if the rat doesn't die when it stops grabbing.
    private void PushAwayFromPlayer()
    {
        this.LookAtPlayerLaterally();
        this.agent.Move(this.transform.forward * -this.distanceOnUnlatch);
    }

    // Has the player flailed their arms around enough?
    private bool StillGrabbing()
    {
        return this.shakeOffThresholdSoFar < this.shakeOffThreshold;
    }

    // Measure how much the player's arm flailing impacts the grabing.
    private void OnShakenOff(float input)
    {
        if (this.currentState == RatState.Grabbing && input >= this.minSpeedToShakeOff)
        {
            this.shakeOffThresholdSoFar += input * Time.deltaTime;
        }
    }

    // Scrolls kill the rat
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.Contains("Scroll"))
        {
            this.Die();
        }
    }
    
    // Getting kicked while grabbing the player just makes them let go faster.
    protected override void GetKicked(IPlayerKick kickData)
    {
        switch(this.currentState)
        {
            case RatState.Chasing:
                base.GetKicked(kickData);
                break;
            case RatState.Grabbing:
                this.OnShakenOff(this.minSpeedToShakeOff + (this.kickWeightForShakeOff * kickData.GetKickDistance()));
                break;
            default:
                base.GetKicked(kickData);
                break;

        }
    }

}
