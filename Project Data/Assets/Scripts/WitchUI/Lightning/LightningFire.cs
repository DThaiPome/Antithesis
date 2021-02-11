using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningFire : MonoBehaviour
{
    [SerializeField]
    private int damagePerSecond = 4;

    private float secondsLeftForDamage;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collapsible Tree"))
        {
            CollapsingTreeBehaviour ctb = other.GetComponent<CollapsingTreeBehaviour>();
            if (!ctb.IsKnockedDown())
            {
                ctb.KnockDown();
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.UpdatePlayer();
        }
    }

    void Update()
    {
        this.secondsLeftForDamage = Mathf.Max(0, this.secondsLeftForDamage - Time.deltaTime);
    }

    private void UpdatePlayer()
    {
        if (secondsLeftForDamage == 0)
        {
            EventChannel.current.onDamagePlayer(this.damagePerSecond);
            this.secondsLeftForDamage = 1;
        }
    }
}
