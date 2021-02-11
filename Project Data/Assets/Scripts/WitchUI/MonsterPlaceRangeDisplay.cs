using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPlaceRangeDisplay : AMonsterPlaceRangeDisplay
{

    public override void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }

    public override void SetRadius(float radius)
    {
        this.transform.localScale = this.GetScaleVector(radius);
    }

    private Vector3 GetScaleVector(float radius)
    {
        float length = radius * 2;
        return new Vector3(length, this.transform.localScale.y, length);
    }

}
