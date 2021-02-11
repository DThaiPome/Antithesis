using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private int arraySize = 8;
    private int currentHolding = 0;
    private int activeIndex = 0;

    private Transform itemSlotContainer;
    private Transform[] itemSlotTemplate;

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        itemSlotContainer = transform;
        itemSlotTemplate = new Transform[arraySize];

        for (int i = 0; i < arraySize; i++)
        {
            string name = (i + 1).ToString();
            itemSlotTemplate[i] = itemSlotContainer.Find("Slot" + name).Find("Text");
        }

        RefreshInvItems();
    }

    private void RefreshInvItems()
    {
        int component = 0;
        foreach (Item i in inventory.GetList()) {
            string itemDisplay = i.GetItemName();
            //legacy
            /*if (i.CheckActive())
            {
                itemDisplay += " (active)";
            }*/
            string numberName = component.ToString();
            if (activeIndex == component)
            {
                itemSlotTemplate[component].parent.Find("Active").GetComponent<UnityEngine.UI.Text>().text = "(active)";
            } 
            else
            {
                itemSlotTemplate[component].parent.Find("Active").GetComponent<UnityEngine.UI.Text>().text = "";
            }
            itemSlotTemplate[component].GetComponent<UnityEngine.UI.Text>().text = itemDisplay;
            component++;
        }
    }

    public void SetActiveIndex(int index)
    {
        activeIndex = index;
    }
}
