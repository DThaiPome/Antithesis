using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Generic implementation of a stat buff - does nothing!
public abstract class AStatBuff : IStatBuff
{
    public virtual int GetDex(int dex)
    {
        return dex;
    }

    public virtual int GetHP(int hp)
    {
        return hp;
    }

    public virtual int GetStr(int str)
    {
        return str;
    }

    public virtual IStatBuff Update(float deltaTime)
    {
        return this;
    }
}
