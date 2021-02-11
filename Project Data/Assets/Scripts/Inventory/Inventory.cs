using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private Item[] itemList;
    private int maxSize = 8;

    public Inventory()
    {
        itemList = new Item[maxSize];
        for (int i = 0; i < maxSize; i++)
        {
            itemList[i] = new Item("", 0);
        }
    }

    public bool AddItem(Item item, int index)
    {
        if (itemList[index].GetItemType() == "")
        {
            itemList[index] = item;
            return true;
        }
        return false;
    }

    public bool RemoveItem(int index)
    {
        if (itemList[index].GetItemType() != "")
        {
            itemList[index] = new Item("", 0);
            return true;
        }
        return false;
    }

    public Item[] GetList()
    {
        return itemList;
    }

    public int GetMaxSize()
    {
        return maxSize;
    }
}
