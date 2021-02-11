using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Example of a buff - adds a certain amount to the armor score of a stat block
public class ExtraArmorBuff : AStatBuff
{
    private int additionalArmor;

    //Create a buff with the given amount of additional armor.
    public ExtraArmorBuff(int additionalArmor)
    {
        this.additionalArmor = additionalArmor;
    }
}
