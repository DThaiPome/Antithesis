using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayKickPreview : ADisplayKickPreview
{
    [SerializeField]
    private GameObject leftLine;
    [SerializeField]
    private GameObject rightLine;

    private float distance;
    private float angle;

    void OnEnable()
    {
        this.ConfigureLines();
    }

    void Update()
    {
        this.ConfigureLines();
    }

    private void ConfigureLines()
    {
        this.ScaleLine(this.rightLine);
        this.ScaleLine(this.leftLine);
        this.RotateLine(this.rightLine, true);
        this.RotateLine(this.leftLine, false);
        this.PositionLine(this.rightLine, true);
        this.PositionLine(this.leftLine, false);
    }

    private void ScaleLine(GameObject line)
    {
        line.transform.localScale = new Vector3(
            line.transform.localScale.x, line.transform.localScale.y, this.distance / 10f);
    }

    private void RotateLine(GameObject line, bool isRight)
    {
        float multiplier = isRight ? 1 : -1;
        Quaternion newRot = Quaternion.AngleAxis(multiplier * this.angle / 2, Vector3.up);
        line.transform.localRotation = newRot;
    }

    private void PositionLine(GameObject line, bool isRight)
    {
        float multiplier = isRight ? 1 : -1;
        float halfRange = this.distance / 2;
        float halfAngle = (this.angle / 2) * Mathf.Deg2Rad;
        float newX = halfRange * Mathf.Sin(multiplier * halfAngle);
        float newZ = halfRange * Mathf.Cos(multiplier * halfAngle);

        line.transform.localPosition = new Vector3(newX, line.transform.localPosition.y, newZ);
    }

    public override void SetDisplayEnabled(bool enabled)
    {
        this.gameObject.SetActive(enabled);
    }

    public override void SetKickRange(float distance, float angle)
    {
        this.distance = distance;
        this.angle = angle;
    }
}
