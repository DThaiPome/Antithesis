using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpell : ALightningSpell
{
    [SerializeField]
    private float lightningCooldownSeconds = 60;
    [SerializeField]
    private GameObject firePrefab;
    [SerializeField]
    private float fireDurationSeconds = 45;

    private float cooldownLeft;

    void Update()
    {
        this.cooldownLeft = Mathf.Max(0, this.cooldownLeft - Time.deltaTime);
    }

    public override void Cast(Vector3 origin)
    {
        if (this.cooldownLeft == 0)
        {
            this.Lightning(origin);
            this.cooldownLeft = this.lightningCooldownSeconds;
        }
    }

    private void Lightning(Vector3 origin)
    {
        GameObject g = Instantiate(this.firePrefab, origin, this.firePrefab.transform.rotation);
        Destroy(g, this.fireDurationSeconds);
    }

    public override float GetCooldownRemaining()
    {
        return this.cooldownLeft;
    }

    public override float GetMaxCooldown()
    {
        return this.lightningCooldownSeconds;
    }
}
