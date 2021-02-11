using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// represent a tank and its behviors (reuse for goblin)
/// </summary>
public class TankScript : AEnemyBehaviour
{
    [SerializeField] // the distance to attack the player 
    float distanceToAttack = 1f; 
    [SerializeField] // the amount the player will be damaged
    int damage = 5;

    private TankState state; // represents the current state of the tank: chasing or attacking
    public static event Action<int> onDamagePlayerEvent;

    private enum TankState { Chasing, Attacking };

    protected override float GetMoveSpeed()
    {
        switch (this.state)
        {
            case TankState.Chasing:
                return AStatBlock.DexToSpeed(this.statBlock.GetDex());
            case TankState.Attacking:
                return 0;
            default:
                return 0;
        }
    }

    protected override Vector3 GetDestination()
    {
        switch (this.state)
        {
            case TankState.Chasing:
                return this.playerTransform.position;
            case TankState.Attacking:
                return this.transform.position;
            default:
                return this.transform.position;
        }
    }

    protected override void AfterStart()
    {
        base.AfterStart();
        this.InitTank();
    }

    private void InitTank()
    {
        this.state = TankState.Chasing;
    }

    private void UpdateTankState()
    {
        switch (this.state)
        {
            case TankState.Chasing:
                this.ChasingUpdate();
                break;
            case TankState.Attacking:
                this.AttackingUpdate();
                break;
            default:
                break;
        }

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Goblin")
        {
            Destroy(this.gameObject);
        }
    }

    private void ChasingUpdate()
    {
        this.LookAtPlayerLaterally();
        if (Vector3.Distance(this.transform.position, this.playerTransform.position) < this.distanceToAttack)
        {
            this.Attack();
        }
    }

    private void Attack()
    {
        this.state = TankState.Attacking;
        this.LookAtPlayerLaterally();
        EventChannel.current.onDamagePlayer(damage);
    }

    private void AttackingUpdate()
    {
        if (Vector3.Distance(this.transform.position, this.playerTransform.position) > this.distanceToAttack)
        {
            this.state = TankState.Chasing;
        }
    }



}
