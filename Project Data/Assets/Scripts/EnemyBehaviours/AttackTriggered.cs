using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTriggered : MonoBehaviour
{
    bool collided = false;
    public int damage = 5;
    int waitTime = 5;

    float timeColliding;

    [SerializeField] 
    float timeThreshold = 5f;

    public AudioClip gremlinNoise;

    public AudioClip tankNoise;

    public AudioClip wraithNoise;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            timeColliding = 0f;
            EventChannel.current.onDamagePlayer(damage);
            if (this.gameObject.tag == "Goblin")
            {
                AudioSource.PlayClipAtPoint(gremlinNoise, this.transform.position);
            }
            else if (this.gameObject.tag == "Tank")
            {
                AudioSource.PlayClipAtPoint(tankNoise, this.transform.position);
            }
            else
            {
                AudioSource.PlayClipAtPoint(wraithNoise, this.transform.position);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(timeColliding < timeThreshold)
            {
                timeColliding += Time.deltaTime;
            }
            else
            {
                EventChannel.current.onDamagePlayer(damage);
                if(this.gameObject.tag == "Goblin")
                {
                    AudioSource.PlayClipAtPoint(gremlinNoise, this.transform.position);
                } 
                else if(this.gameObject.tag == "Tank")
                {
                    AudioSource.PlayClipAtPoint(tankNoise, this.transform.position);
                } else
                {
                    AudioSource.PlayClipAtPoint(wraithNoise, this.transform.position);
                }
                timeColliding = 0f;
            }
        }
    }
}
