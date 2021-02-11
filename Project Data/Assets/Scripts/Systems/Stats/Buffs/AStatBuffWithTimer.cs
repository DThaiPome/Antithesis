using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A buff that destroys itself after a certain number seconds.
public class AStatBuffWithTimer : AStatBuff
{
    private float secondsLeft;

    /// <summary>
    /// Creates a buff that disappears after a certain number of seconds.
    /// </summary>
    /// <param name="durationSeconds"></param>
    public AStatBuffWithTimer(float durationSeconds)
    {
        this.secondsLeft = durationSeconds;
    }

    public override IStatBuff Update(float deltaTime)
    {
        this.secondsLeft -= deltaTime;
        if (this.secondsLeft <= 0)
        {
            return null;
        }
        return this;
    }
}
