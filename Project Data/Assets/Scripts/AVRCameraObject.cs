using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used for behaviour for when the camera interacts with a wall.
/// </summary>
[RequireComponent(typeof(Camera))]
public abstract class AVRCameraObject : MonoBehaviour
{
    protected Camera camera;

    void Start()
    {
        this.camera = this.GetComponent<Camera>();

        this.AfterStart();
    }

    protected virtual void AfterStart() { }

    /// <summary>
    /// Returns true if the camera is in a wall, and should be blacked out.
    /// </summary>
    /// <returns>true if the camera is in a wall, false otherwise.</returns>
    public abstract bool IsInWall(Vector3 headOrigin);
}
