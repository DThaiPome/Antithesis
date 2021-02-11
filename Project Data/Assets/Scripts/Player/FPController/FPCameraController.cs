using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCameraController : MonoBehaviour
{
    [SerializeField]
    private float mouseYSensitivity = 1000;
    [SerializeField]
    private float maxAngle = 45;
    [SerializeField]
    private float minAngle = -45;
    [SerializeField]
    private string cameraVerticalInput = "CameraVertical";

    private float currentAngle;

    void Start()
    {
        this.currentAngle = this.transform.localEulerAngles.x;
    }

    void Update()
    {
        float input = this.GetVerticalMouseInput();
        this.LookVertically(input);
    }

    private float GetVerticalMouseInput()
    {
        return -Input.GetAxis(this.cameraVerticalInput);
    }

    private void LookVertically(float mouseInput)
    {
        currentAngle = Mathf.Clamp(currentAngle + mouseInput * this.mouseYSensitivity * Time.deltaTime, this.minAngle, this.maxAngle);

        Quaternion rot = Quaternion.AngleAxis(this.currentAngle, Vector3.right);
        this.transform.localRotation = rot;
    }
}
