using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraDisplay : MonoBehaviour
{

    public Camera camera1;
    public int displayNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        camera1.targetDisplay = displayNumber;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
