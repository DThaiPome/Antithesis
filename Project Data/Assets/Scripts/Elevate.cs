using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevate : MonoBehaviour
{
    public float time;
    public float bottomYPosn;
    public float topYPosn;
    public bool comeUp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (comeUp)
        {
            if (transform.position.y != topYPosn)
            {
                if (Mathf.Abs(topYPosn - transform.position.y) < 0.1)
                {
                    transform.position = new Vector3(transform.position.x, topYPosn, transform.position.z);
                } else
                {
                    transform.position = transform.position + new Vector3(0, (topYPosn - bottomYPosn) * (1 / time) * Time.deltaTime, 0);
                }
            }
        } 
        else
        {
            if (transform.position.y != bottomYPosn)
            {
                if (Mathf.Abs(bottomYPosn - transform.position.y) < 0.1)
                {
                    transform.position = new Vector3(transform.position.x, bottomYPosn, transform.position.z);
                }
                else
                {
                    transform.position = transform.position + new Vector3(0, (bottomYPosn - topYPosn) * (1 / time) * Time.deltaTime, 0);
                }
            }
        }
    }
}
