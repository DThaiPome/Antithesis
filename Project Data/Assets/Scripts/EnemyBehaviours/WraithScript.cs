using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// represent a wraith and its behviors (reuse for goblin)
/// </summary>
public class WraithScript : AEnemyBehaviour
{
    [SerializeField] // the distance to attack the player 
    float distanceToAttack = 1f;

    private wraithState state; // represents the current state of the wraith: chasing or attacking

    private bool attacked;
    private enum wraithState { Chasing, Attacking };

    public GameObject fog;

    protected override float GetMoveSpeed()
    {
        switch (this.state)
        {
            case wraithState.Chasing:
                return AStatBlock.DexToSpeed(this.statBlock.GetDex());
            case wraithState.Attacking:
                return 0;
            default:
                return 0;
        }
    }

    protected override Vector3 GetDestination()
    {
        switch (this.state)
        {
            case wraithState.Chasing:
                return this.playerTransform.position;
            case wraithState.Attacking:
                return this.transform.position;
            default:
                return this.transform.position;
        }
    }

    protected override void AfterStart()
    {
        base.AfterStart();
        this.Initwraith();
    }

    private void Initwraith()
    {
        this.state = wraithState.Chasing;
        EventChannel.current.spawnWraithFogEvent += this.OnSpawn;
    }

    protected override void AfterUpdate()
    {
        base.AfterUpdate();
        this.UpdatewraithState();
    }

    protected override void AfterDestroy()
    {
        base.AfterDestroy();
        EventChannel.current.spawnWraithFogEvent -= this.OnSpawn;
    }

    private void UpdatewraithState()
    {
        switch (this.state)
        {
            case wraithState.Chasing:
                this.ChasingUpdate();
                break;
            case wraithState.Attacking:
                this.AttackingUpdate();
                break;
            default:
                break;
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
        this.state = wraithState.Attacking;
        if (!this.attacked)
        {
            this.attacked = true;
            this.LookAtPlayerLaterally();
        }
    }

    private void AttackingUpdate()
    {
        if (Vector3.Distance(this.transform.position, this.playerTransform.position) > this.distanceToAttack)
        {
            this.attacked = false;
            this.state = wraithState.Chasing;
        }
    }

    private void OnSpawn(GameObject fog)
    {
        GameObject wriathFog = GameObject.Instantiate(this.fog, this.transform.position, transform.rotation) as GameObject;
    }

}
