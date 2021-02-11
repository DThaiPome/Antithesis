using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchCamera : MonoBehaviour
{
    void Awake()
    {
        this.GetComponent<Camera>().stereoTargetEye = StereoTargetEyeMask.None;
    }
}
