using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDisplays : ADisplayManager
{
    [SerializeField]
    private int displayCount = 1;

    void Start()
    {
        ActivateDisplays(displayCount);
    }

    //Activates displays within [0, count)
    private static void ActivateDisplays(int count)
    {
        for(int i = 0; i < count; i++)
        {
            Display.displays[i].Activate();
        }
    }

    public override void SetDisplays(int count)
    {
        ActivateDisplays(this.displayCount);

    }
}
