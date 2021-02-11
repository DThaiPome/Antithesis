using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonVRSwitchDisplays : MonoBehaviour
{
    public Camera cam1;
    public Camera cam2;
    public GameObject nonVRplayer;

    public Canvas witchPlay;
    public Canvas witchWin;
    public Canvas witchLose;

    
    private bool witchView;

    void Start()
    {

        if (nonVRplayer.activeSelf)
        {
            witchView = false;
            cam1.targetDisplay = 0;
            cam2.targetDisplay = 0;
        }
    }

    void Update()
    {
        if (nonVRplayer.activeSelf)
        {
            if (witchView)
            {
                cam1.enabled = false;
                cam2.enabled = true;
                
                witchPlay.targetDisplay = 0;
                witchLose.targetDisplay = 0;
                witchWin.targetDisplay = 0;
            }

            if (!witchView)
            {
                cam1.enabled = true;
                cam2.enabled = false;

                witchPlay.targetDisplay = 1;
                witchLose.targetDisplay = 1;
                witchWin.targetDisplay = 1;
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                
                witchView = !witchView;
                

            }
        }
        
    }
}
