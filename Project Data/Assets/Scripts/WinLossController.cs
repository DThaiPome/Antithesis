using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WinLossController : MonoBehaviour
{
    public GameObject[] items = new GameObject[5];
    private Dictionary<GameObject, Boolean> itemsIndexed = new Dictionary<GameObject, bool>();


    void Start()
    {
        //FIX MAKE RECOGNIZE ALL
        foreach (GameObject g in items)
        {
            itemsIndexed.Add(g, false);
        }
    }

    void Update()
    {
        bool win = true;
        for (int i = 0; i < itemsIndexed.Count; i++)
        {
            if (!itemsIndexed[items[i]])
            {
                win = false;
            }
        }

        if (win && !GameManager.ritualComplete)
        {
            winGame();
        }
    }

    void winGame()
    {
        Debug.Log("PLAYER WINS PLAYER WINS PLAYER WINS");
        GameManager.SetRitualComplete();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Win Item"))
        {
            itemsIndexed[other.gameObject] = true;
            
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Win Item"))
        {
            //itemsIndexed[collision.gameObject] = false;

        }
    }
}
