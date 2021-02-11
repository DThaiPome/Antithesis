using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRCameraObject : AVRCameraObject
{
    [SerializeField]
    private string[] wallLayerNames;

    public override bool IsInWall(Vector3 originPosition)
    {
        Vector3 toOrigin = (originPosition - this.transform.position);
        Ray ray = new Ray(originPosition, -toOrigin.normalized);

        bool hit = Physics.Raycast(ray, toOrigin.magnitude, LayerMask.GetMask(this.wallLayerNames));
        return hit;
    }
}
