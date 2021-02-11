using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Rat"
         || collision.gameObject.tag == "Goblin"
         || collision.gameObject.tag == "Tank"
         || collision.gameObject.tag == "Caster")
        {
            Destroy(this.gameObject);
        }
    }
}
