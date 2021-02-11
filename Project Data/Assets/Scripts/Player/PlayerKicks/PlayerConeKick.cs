using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Kicks within the range of a flat, 2 dimensional cone that fans
/// out from a certain point on the player.
/// </summary>
public class PlayerConeKick : APlayerKick
{
    private Transform playerTransform;

    private float kickHeight;
    private float kickAngle;
    private float kickRange;

    public PlayerConeKick(float kickDistance, Transform playerTransform, float kickHeight, float kickAngle, float kickRange) : base(kickDistance)
    {
        this.playerTransform = playerTransform;
        this.kickHeight = kickHeight;
        this.kickAngle = kickAngle;
        this.kickRange = kickRange;
    }

    public override bool WithinKick(GameObject self)
    {
        Vector3 kickOrigin = new Vector3(
            this.playerTransform.position.x, 
            Mathf.Min(this.playerTransform.position.y + kickHeight, self.transform.position.y), 
            this.playerTransform.position.z);
        Vector3 kickToSelf = (self.transform.position - kickOrigin);
        Vector3 lateralKickToSelf = new Vector3(
            kickToSelf.x, 
            0,
            kickToSelf.z);
        Vector3 kickCheckDirection = lateralKickToSelf.normalized;

        float distanceFromPlayer = lateralKickToSelf.magnitude;
        float angleFromPlayer = Vector3.Angle(this.playerTransform.forward, lateralKickToSelf);

        if (distanceFromPlayer > kickRange || angleFromPlayer > kickAngle / 2)
        {
            return false;
        }

        Ray ray = new Ray(kickOrigin, kickCheckDirection);
        RaycastHit[] hits = Physics.RaycastAll(ray, this.kickRange);
        foreach(RaycastHit hit in hits)
        {
            if (hit.collider.gameObject == self)
            {
                return true;
            }
        }
        return false;
    }
}
