using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasterScript : AEnemyBehaviour
{
    [SerializeField] private float attackDistance = 10f;
    [SerializeField] private float AttackTimeCooldown = 3.0f;
    public GameObject projectile;
    public Transform firePoint;
    public float shootForce = 50f;
    private CasterState state;
    public float shootingRate = 10f;
    private Transform initParent;
    public static event Action onDamagePlayerEvent;
    public float Timer = 2f;

    private enum CasterState
    {
        Chasing, Shooting
    }

    protected override float GetMoveSpeed()
    {
        switch (this.state)
        {
            case CasterState.Chasing:
                return AStatBlock.DexToSpeed(this.statBlock.GetDex());
            case CasterState.Shooting:
                return 0;
            default:
                return 0;
        }
    }

    protected override Vector3 GetDestination()
    {
        return this.playerTransform.position;
    }

    protected override void AfterStart()
    {
        base.AfterStart();
        this.InitCaster();
    }

    private void InitCaster()
    {
        this.state = CasterState.Chasing;
        this.initParent = this.transform.parent;
    }

    protected override void AfterUpdate()
    {
        base.AfterUpdate();
        Timer -= Time.deltaTime;
        this.UpdateCasterCurrentState();
    }

    private void UpdateCasterCurrentState()
    {
        switch (this.state)
        {
            case CasterState.Chasing:
                this.ChasingUpdate();
                break;
            case CasterState.Shooting:
                while (Timer <= 0f)
                {
                    this.StartShooting();
                }
                break;
            default:
                break;
        }
    }

    private void ChasingUpdate()
    {
        this.transform.LookAt(playerTransform.transform);

            if (Vector3.Distance(this.transform.position, this.playerTransform.position) <= this.attackDistance)
            {
                this.StartShooting();
            }

    }

    private void StartShooting()
    {
            this.transform.LookAt(playerTransform.transform);
            this.state = CasterState.Shooting;
            GameObject shot = GameObject.Instantiate(projectile, firePoint.position, transform.rotation) as GameObject;
            shot.GetComponent<Rigidbody>().AddForce(transform.forward * shootForce);
            Timer = 2f;
    }

    private void ShootingUpdate()
    {
        if (Vector3.Distance(this.transform.position, this.playerTransform.position) > this.attackDistance)
        {
            this.state = CasterState.Chasing;
        }

    }


}
