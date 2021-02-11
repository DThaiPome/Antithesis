using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(CapsuleCollider))]
public class PositionVRRigidbody : MonoBehaviour
{
    [SerializeField]
    private CharacterController vrCharacterController;

    private CapsuleCollider cc;

    void Start()
    {
        this.cc = this.GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        this.cc.height = this.vrCharacterController.height;
        this.transform.localPosition = this.vrCharacterController.center;
    }

}
