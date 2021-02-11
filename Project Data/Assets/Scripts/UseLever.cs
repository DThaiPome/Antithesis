using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseLever : MonoBehaviour
{
    public bool activated = false;
    public Elevate toElevate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.z * (180 / Mathf.PI) <= -21)
        {
            if (!activated)
            {
                activated = true;
                toElevate.comeUp = true;
                Debug.Log("Triggered");
            }
        }
        else if (transform.rotation.z * (180 / Mathf.PI) >= 21)
        {
            if (activated)
            {
                activated = false;
                toElevate.comeUp = false;
                Debug.Log("Turned off");
            }
        }
    }
   
}
