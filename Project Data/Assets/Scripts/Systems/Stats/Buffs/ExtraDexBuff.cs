using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraDexBuff : AStatBuff
{
    private int additionalDex;

    //Create a buff with the given amount of additional armor.
    public ExtraDexBuff(int additionalDex)
    {
        this.additionalDex = additionalDex;
    }

    public override int GetDex(int dex)
    {
        return dex + additionalDex;
    }
}
