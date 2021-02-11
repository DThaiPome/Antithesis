using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AStatBlock))]
public abstract class AEnemyBehaviour : MonoBehaviour
{
    protected AStatBlock statBlock;
    protected NavMeshAgent agent;
    protected Vector3 destination;
    protected Vector3 pushBackEight = new Vector3(8f, 8f, 8f);
    protected bool isCollidedBear;
    protected bool isCollidedScroll;
    protected float timeStunBear = 7;
    protected float timeStunScroll = 13;
    protected Transform playerTransform;

    void Start()
    {
        this.statBlock = this.GetComponent<AStatBlock>();
        this.agent = this.GetComponent<NavMeshAgent>();
        this.playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        this.agent.speed = this.GetMoveSpeed();

        EventChannel.current.onAttackObjectEvent += this.OnAttacked;
        EventChannel.current.onPlayerKickEvent += this.OnKick;

        this.AfterStart();
    }

    void OnDestroy()
    {
        EventChannel.current.onAttackObjectEvent -= this.OnAttacked;
        EventChannel.current.onPlayerKickEvent -= this.OnKick;
    }

    /// <summary>
    /// Implement this to add more functionality at Start time.
    /// </summary>
    protected virtual void AfterStart() { }

    /// <summary>
    /// Implement this to add more functionality once the object is destroyed;
    /// </summary>
    protected virtual void AfterDestroy() { }

    void Update()
    {
        this.destination = this.GetDestination();
        this.agent.SetDestination(this.destination);
        this.checkCollision();
        this.AfterUpdate();

        if (GameManager.isGameWon() || GameManager.isGameLost())
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Implement this to add more functionality after every frame.
    /// </summary>
    protected virtual void AfterUpdate() { }

    /// <summary>
    /// Returns the current movement speed of this enemy.
    /// </summary>
    /// <returns>speed in unit</returns>
    protected abstract float GetMoveSpeed();

    /// <summary>
    /// Get the next destination that this enemy should move towards
    /// </summary>
    /// <returns>a position</returns>
    protected abstract Vector3 GetDestination();

    protected virtual void OnAttacked(GameObject obj, int damage)
    {
        if (obj.Equals(this.gameObject))
        {
            this.DamageTaken(damage);
        }
    }

    protected virtual void DamageTaken(int damage)
    {
        this.statBlock.ModifyHP(-damage);
        this.statBlock.AddBuff(new MultiplyDexBuff(0.25f, 0.2f));
    }

    protected virtual void LookAtPlayerLaterally()
    {
        Vector3 playerPos = this.playerTransform.position;
        Vector3 target = new Vector3(playerPos.x, this.transform.position.y, playerPos.z);
        Vector3 toTarget = (target - this.transform.position).normalized;
        Quaternion rotation = toTarget == Vector3.zero ? this.transform.rotation : Quaternion.LookRotation(toTarget, this.transform.up);
        this.transform.rotation = rotation;
    }

    protected virtual void Die()
    {
        EventChannel.current.onEnemyDestroyed(this.tag);

        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.name.Contains("Bear"))
        {
            isCollidedBear = true;
        } 
        else if (col.gameObject.name.Contains("Scroll"))
        {
            Debug.Log(this.gameObject.name);
            this.agent.nextPosition = this.destination - this.pushBackEight;
            isCollidedScroll = true;
        }
    }

    protected virtual void checkCollision()
    {
        if (isCollidedBear)
        {
            if (timeStunBear >= 0)
            {
                this.agent.speed = 0;
                timeStunBear -= Time.deltaTime;
            }
            else
            {
                timeStunBear = 7;
                this.agent.speed = this.GetMoveSpeed();
                isCollidedBear = false;
            }
        }
        else if (isCollidedScroll)
        {
            if (timeStunScroll >= 0)
            {
                this.agent.speed = this.GetMoveSpeed() / 2;
                timeStunScroll -= Time.deltaTime;
            }
            else
            {
                timeStunBear = 13;
                this.agent.speed = this.GetMoveSpeed();
                isCollidedBear = false;
            }
        }
    }

    protected virtual void OnKick(IPlayerKick kickData)
    {
        if (kickData.WithinKick(this.gameObject))
        {
            this.GetKicked(kickData);
        }
    }

    protected virtual void GetKicked(IPlayerKick kickData)
    {
        this.LookAtPlayerLaterally();
        float distanceToGetKicked = AStatBlock.DexToSpeed(this.statBlock.GetDex());
        this.agent.Move(-this.transform.forward * distanceToGetKicked);
    }
}
