using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrapController : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name.Contains("Caster") 
        || col.gameObject.name.Contains("Goblin")
        || col.gameObject.name.Contains("Rat")
        || col.gameObject.name.Contains("Tank"))
        {
            Destroy(this.gameObject);
        }
    }
}
