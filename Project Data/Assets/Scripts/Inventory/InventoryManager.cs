using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Inventory inv;
    [SerializeField] private UI_Inventory uiInventory;
    private Item activeItem;
    public int activeIndex = 0;
    public bool isVR = true;
    public string cameraName;
    public ItemWorld heldItem = null;
    [SerializeField]
    private string stash;
    [SerializeField]
    private string drop;
    [SerializeField]
    private string menuScrollLeft;
    [SerializeField]
    private string menuScrollRight;

    void Start()
    {
        inv = new Inventory();
        activeItem = inv.GetList()[activeIndex];
        activeItem.SetActive();
        uiInventory.SetInventory(inv);
    }

    // Update is called once per frame
    void Update()
    {
        CheckActiveItem();
        uiInventory.SetActiveIndex(activeIndex);

        if (Input.GetKeyDown(drop))
        {
            if (!isVR)
            {
                Instantiate(activeItem.GetObjectAsset(), GameObject.Find(cameraName).transform.position, new Quaternion(0, 0, 0, 0));
                inv.RemoveItem(activeIndex);
                inv.GetList()[activeIndex].SetActive();
                activeItem = inv.GetList()[activeIndex];
            }
            else
            {
                Debug.Log(activeItem.GetItemType());
                activeItem.GetObjectAsset().SetActive(true);
                activeItem.GetObjectAsset().transform.position = GameObject.Find(cameraName).transform.position;
                if (activeItem.GetObjectAsset().CompareTag("Win Item"))
                {
                    activeItem.GetObjectAsset().transform.parent = GameObject.Find("RitualComponents").transform;
                }
                else if (activeItem.GetObjectAsset().CompareTag("Potion"))
                {
                    activeItem.GetObjectAsset().transform.parent = GameObject.Find("Potions").transform;
                }
                else if (activeItem.GetObjectAsset().CompareTag("Trap"))
                {
                    activeItem.GetObjectAsset().transform.parent = GameObject.Find("Traps").transform;
                }
                inv.RemoveItem(activeIndex);
                inv.GetList()[activeIndex].SetActive();
                activeItem = inv.GetList()[activeIndex];
            }
        }
        if (Input.GetKeyDown(stash))
        {
            if (activeItem.GetItemType() == "")
            {
                if (!isVR)
                {
                    ItemWorld heldItem = GameObject.Find("ItemGrabber").transform.GetChild(0).GetComponent<ItemWorld>();
                    AddItem(new Item(heldItem.type, 1));
                    uiInventory.SetInventory(inv);

                    foreach (Transform child in GameObject.Find("ItemGrabber").transform)
                    {
                        Destroy(child.gameObject);
                    }
                } 
                else
                {
                    if (heldItem != null)
                    {
                        AddItem(new Item(heldItem.type, 1));
                        uiInventory.SetInventory(inv);

                        heldItem.gameObject.SetActive(false);
                        //Destroy(heldItem.gameObject);
                        heldItem = null;
                    }
                }
            }
        }
    }

    void CheckActiveItem()
    {
        if (Input.GetKeyDown(menuScrollLeft))
        {
            activeIndex = Mathf.Clamp(activeIndex - 1, 0, inv.GetMaxSize() - 1);
            activeItem.Deactivate();
            activeItem = inv.GetList()[activeIndex];
            activeItem.SetActive();
        }
        else if (Input.GetKeyDown(menuScrollRight))
        {
            activeIndex = Mathf.Clamp(activeIndex + 1, 0, inv.GetMaxSize() - 1);
            activeItem.Deactivate();
            activeItem = inv.GetList()[activeIndex];
            activeItem.SetActive();
        }

        uiInventory.SetInventory(inv);
    }

    public void AddItem(Item i)
    {
        if (inv.AddItem(i, activeIndex))
        {
            activeItem.Deactivate();
            i.SetActive();
            activeItem = i;
        }
    }

    public Item GetActiveItem()
    {
        return activeItem;
    }

    public void SetHeld(ItemWorld w)
    {
        heldItem = w;
    }
}
