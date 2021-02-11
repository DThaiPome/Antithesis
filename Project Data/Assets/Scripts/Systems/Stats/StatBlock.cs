using System.Collections;
using System.Collections.Generic;
//using System.Runtime.Remoting.Messaging;
using UnityEngine;

//A stat block that starts with initial values
public class StatBlock : AStatBlockWithBuffs
{
    [SerializeField]
    protected int hp = 200;
    [SerializeField]
    protected int maxHitPoints = 200;
    [SerializeField]
    protected int dexterityScore = 12;
    [SerializeField] 
    private int strengthScore = 10;

    //Buffs
    protected List<IStatBuff> buffs;

    protected override void AfterStart()
    {
        base.AfterStart();
        this.hp = this.maxHitPoints;
    }

    public override int GetDex()
    {
        return this.DexThruBuffs(this.dexterityScore);
    }

    public override int GetHP()
    {
        return this.HPThruBuffs(this.hp);
    }

    public override int GetStr()
    {
        return this.StrThruBuffs(this.strengthScore);
    }

    //Caps HP within [0, maxHitPoints]
    public override int ModifyHP(int dHP)
    {
        //Should this cap to the max HP?
        this.hp = Mathf.Clamp(this.hp + dHP, 0, this.maxHitPoints);
        return this.hp;
    }

    protected override IEnumerable<IStatBuff> InitBuffCollection()
    {
        return new List<IStatBuff>();
    }
}
