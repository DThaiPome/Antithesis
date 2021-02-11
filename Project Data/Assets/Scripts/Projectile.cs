﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public AudioClip fireBall;

    public float bulletSpeed = 60;
    public float hitForce = 50f;
    public float destroyAfter = 3.5f;
    /// <summary>
    ///  the base damage done to the player when hit by a projectile.
    /// </summary>
    [SerializeField]
    private int damage = 5;

    /// <summary>
    /// The base movement speed of the missile, in units per second. 
    /// </summary>
    [SerializeField]
    private float speed = 15;

    /// <summary>
    /// The base rotation speed of the missile, in radians per second. 
    /// </summary>
    [SerializeField]
    private float rotationSpeed = 1000;

    /// <summary>
    /// The distance at which this object stops following its target and continues on its last known trajectory. 
    /// </summary>
    [SerializeField]
    private float focusDistance = 5;

    /// <summary>
    /// The transform of the target object.
    /// </summary>
    private Transform target;

    /// <summary>
    /// Returns true if the object should be looking at the target. 
    /// </summary>
    private bool isLookingAtObject = true;

    /// <summary>
    /// The tag of the target object.
    /// </summary>
    [SerializeField]
    private string targetTag;

    /// <summary>
    /// Error message.
    /// </summary>
    private string enterTagPls = "Player";

    private void Start()
    {
        AudioSource.PlayClipAtPoint(fireBall, this.transform.position);
        if (targetTag == "")
        {
            Debug.LogError(enterTagPls);
            return;
        }

        target = GameObject.FindGameObjectWithTag(targetTag).transform;
    }

    private void Update()
    {
        if (targetTag == "")
        {
            Debug.LogError(enterTagPls);
            return;
        }

        Vector3 targetDirection = target.position - transform.position;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationSpeed * Time.deltaTime, 0.0F);

        transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);

        if (Vector3.Distance(transform.position, target.position) < focusDistance)
        {
            isLookingAtObject = false;
        }

        if (isLookingAtObject)
        {
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        StartCoroutine(DestroyBullet());
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("triggered");
            EventChannel.current.onDamagePlayer(damage);
            Destroy(this.gameObject);
        }
    }
}

