using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatBlock : StatBlock
{

    //Player has no strength - set strength will be irrelevant
    public override int GetStr()
    {
        return 0;
    }

    protected override void AfterUpdate()
    {
        base.AfterUpdate();

        if (this.GetHP() <= 0)
        {
            GameManager.SetLoseState();
        }
    }
}


