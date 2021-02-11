using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplyDexBuff : AStatBuffWithTimer
{
    private float dexMultiplier;

    public MultiplyDexBuff(float durationSeconds, float dexMultiplier) : base(durationSeconds)
    {
        this.dexMultiplier = dexMultiplier;
    }

    public override int GetDex(int dex)
    {
        return (int)(this.dexMultiplier * dex);
    }
}
