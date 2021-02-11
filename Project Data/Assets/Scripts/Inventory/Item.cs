using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private string itemType;
    private int amount;
    private bool onDisplay;

    public Item(string type, int amt)
    {
        itemType = type;
        amount = amt;
        onDisplay = false;
    }

    public string GetItemName()
    {
        return itemType;
    }

    public GameObject GetObjectAsset()
    {
        return GameObject.Find("ItemAssets").GetComponent<ItemAssets>().Get(itemType);
    }

    public string GetItemType()
    {
        return itemType;
    }

    public void SetActive()
    {
        onDisplay = true;
    }

    public void Deactivate()
    {
        onDisplay = false;
    }

    public bool CheckActive()
    {
        return onDisplay;
    }

}
