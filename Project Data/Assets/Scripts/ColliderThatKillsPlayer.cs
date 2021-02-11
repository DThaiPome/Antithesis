using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderThatKillsPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.KillPlayer();
        } 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            this.KillPlayer();
        }
    }

    private void KillPlayer()
    {
        EventChannel.current.onDamagePlayer(int.MaxValue / 2);
    }
}
