using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlaceKickPreviewAtBottomOfCollider : MonoBehaviour
{
    [SerializeField]
    private ADisplayKickPreview kickPreview;
    [SerializeField]
    private float heightFromBottom = .2f;
    [SerializeField]
    private Transform cameraTransform;

    private CharacterController cc;

    void Awake()
    {
        this.cc = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        float relativeHeight = this.heightFromBottom;
        this.kickPreview.transform.localPosition = new Vector3(
            this.cameraTransform.localPosition.x, relativeHeight, this.cameraTransform.localPosition.z);

        float cameraAngles = this.cameraTransform.eulerAngles.y;
        this.kickPreview.transform.localRotation = Quaternion.AngleAxis(cameraAngles, Vector3.up);
    }
}
