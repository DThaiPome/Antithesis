using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[RequireComponent(typeof(AStatBlock))]
[RequireComponent(typeof(CharacterController))]
public class FPPlayerController : MonoBehaviour
{
    [SerializeField]
    private float mouseXSensitivity = 500;
    [SerializeField]
    private float gravityAccel = -9.18f;
    [SerializeField]
    private string turnInput = "CameraHorizontal";

    private AStatBlock statBlock;
    private CharacterController cc;

    private float yVel;

    void Start()
    {
        this.Init();
    }

    //Initialize component values.
    private void Init()
    {
        this.statBlock = this.GetComponent<AStatBlock>();
        this.cc = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 moveInput = this.GetMoveInput();
        this.Move(moveInput);

        float mouseInput = this.GetHorizontalMouseInput();
        this.Turn(mouseInput);
    }

    //Move based on the given input.
    private void Move(Vector3 input)
    {
        float angleFromZ = Vector2.SignedAngle(Vector2.up, new Vector2(this.transform.forward.x, this.transform.forward.z));
        Vector3 relativeMove = this.RotateVector(input, angleFromZ);
        this.Gravity();
        Vector3 gravityMove = new Vector3(0, this.yVel, 0);
        Vector3 dPos = ((relativeMove * this.GetMoveSpeed()) + gravityMove) * Time.deltaTime;
        this.cc.Move(dPos);
    }

    private void Gravity()
    {
        if (this.cc.isGrounded)
        {
            this.yVel = 0;
        } else
        {
            this.yVel += this.gravityAccel * Time.deltaTime;
        }
    }

    //Rotate this flat vector (no y transformation) r degrees.
    private Vector3 RotateVector(Vector3 v, float deg)
    {
        float r = Mathf.Deg2Rad * deg;
        return new Vector3(
            v.x * Mathf.Cos(r) + v.z * -Mathf.Sin(r),
            v.y,
            v.x * Mathf.Sin(r) + v.z * Mathf.Cos(r));
    }

    //Turn based on the given mouse input.
    private void Turn(float input)
    {
        Quaternion dRot = Quaternion.AngleAxis(
            this.mouseXSensitivity * input * Time.deltaTime, 
            Vector3.up);
        this.transform.rotation *= dRot;
    }
    
    //Returns a vector that represents the movement input
    //as a vector.
    private Vector3 GetMoveInput()
    {
        float horInput = Input.GetAxis("Horizontal");
        float verInput = Input.GetAxis("Vertical");

        Vector3 inputVector = new Vector3(horInput, 0, verInput);
        float mag = this.GetInputSpeed(horInput, verInput);

        return inputVector.normalized * mag;
    }

    //What is the total speed of the two inputs combined?
    private float GetInputSpeed(float x, float y)
    {
        float bX = Mathf.Abs(x);
        float bY = Mathf.Abs(y);
        float sum = bX + bY;
        float sqrSum = (bX * bX) + (bY * bY);
        return sum == 0 ? 0 : sqrSum / sum;
    }
    
    private float GetHorizontalMouseInput()
    {
        return Input.GetAxis(this.turnInput);
    }

    private float GetMoveSpeed()
    {
        return AStatBlock.DexToSpeed(this.statBlock.GetDex());
    }
}
