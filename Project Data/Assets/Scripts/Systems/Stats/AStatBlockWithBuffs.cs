using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class AStatBlockWithBuffs : AStatBlock
{
    protected IEnumerable<IStatBuff> buffs;

    protected override void AfterAwake()
    {
        base.AfterAwake();
        this.buffs = this.InitBuffCollection();
    }

    protected override void AfterStart()
    {
        base.AfterStart();
    }

    protected abstract IEnumerable<IStatBuff> InitBuffCollection();

    protected override void AfterUpdate()
    {
        base.AfterUpdate();
        this.AdvanceBuffs(Time.deltaTime);
    }

    protected virtual void AdvanceBuffs(float deltaTime)
    {
        List<IStatBuff> result = new List<IStatBuff>();
        foreach(IStatBuff buff in this.buffs)
        {
            IStatBuff b = buff.Update(deltaTime);
            if (b != null)
            {
                result.Add(b);
            }
        }
        this.buffs = Enumerable.Empty<IStatBuff>();
        foreach(IStatBuff buff in result)
        {
            this.AddBuffToList(buff);
        }
    }

    protected virtual int HPThruBuffs(int initialHP)
    {
        int result = initialHP;
        foreach(IStatBuff buff in this.buffs)
        {
            result = buff.GetHP(result);
        }
        return result;
    }

    protected virtual int DexThruBuffs(int initialDex)
    {
        int result = initialDex;
        foreach (IStatBuff buff in this.buffs)
        {
            result = buff.GetDex(result);
        }
        return result;
    }

    protected virtual int StrThruBuffs(int initialStr)
    {
        int result = initialStr;
        foreach (IStatBuff buff in this.buffs)
        {
            result = buff.GetStr(result);
        }
        return result;
    }

    public override void AddBuff(IStatBuff buff)
    {
        if (buff != null)
        {
            this.AddBuffToList(buff);
        }
    }

    protected void AddBuffToList(IStatBuff buff)
    {
        IEnumerable<IStatBuff> newBuff = new IStatBuff[] { buff };
        this.buffs = this.buffs.Concat(newBuff);
    }
}
