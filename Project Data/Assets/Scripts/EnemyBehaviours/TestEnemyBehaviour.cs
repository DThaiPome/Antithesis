using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyBehaviour : AEnemyBehaviour
{
    protected bool isCollision = false;
    protected override void AfterUpdate()
    {
        base.AfterUpdate();
        this.transform.LookAt(this.PGetDestination());
        if (this.statBlock.GetHP() <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    protected override float GetMoveSpeed()
    {
       return AStatBlock.DexToSpeed(this.statBlock.GetDex());
    }

    protected override Vector3 GetDestination()
    {
        return this.PGetDestination();
    }

    /// <summary>
    /// A sort of "wrapper" for GetDestination.
    /// </summary>
    private Vector3 PGetDestination()
    {
        return this.playerTransform.position;
    }

}

