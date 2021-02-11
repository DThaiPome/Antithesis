using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    private Item item;
    public string type;
    public bool isVr = false;

    // Start is called before the first frame update
    void Start()
    {
        item = new Item(type, 1);
        isVr = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>().isVR;
    }

    // Update is called once per frame
    void Update()
    {
        if (isVr)
        {
            if (transform.parent == null)
            {
                //Debug.Log(type + " Picked up!");
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>().SetHeld(this);
            }
            else
            {
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>().heldItem == this)
                {
                    //Debug.Log("Dropping " + type);
                    GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>().SetHeld(null);
                }
            }
        }
    }

    public Item GetItem()
    {
        return item;
    }
}
