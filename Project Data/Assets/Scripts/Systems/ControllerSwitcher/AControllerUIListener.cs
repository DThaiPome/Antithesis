using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AControllerUIListener : MonoBehaviour
{
    public abstract void SetVRControls();

    public abstract void SetNonVRControls();

    public abstract void StartGame();

    public abstract void StartVRGame();

    public abstract void StartNonVRGame();
}
