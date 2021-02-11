using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }
    public GameObject box;
    public GameObject can;
    public GameObject bucket;
    public GameObject goblet;
    public GameObject flower;
    public GameObject necklace;
    public GameObject speedPotion;
    public GameObject healthPotion;
    public GameObject scrollTrap;
    public GameObject bearTrap;
    public Dictionary<string, GameObject> map = new Dictionary<string, GameObject>();

    private void Awake()
    {
        Instance = this;
        map.Add("box", box);
        map.Add("can", can);
        map.Add("Bucket", bucket);
        map.Add("Goblet", goblet);
        map.Add("Flower", flower);
        map.Add("Necklace", necklace);
        map.Add("Speed Potion", speedPotion);
        map.Add("Health Potion", healthPotion);
        map.Add("Scroll Trap", scrollTrap);
        map.Add("Bear Trap", bearTrap);
    }

    public GameObject Get(string name)
    {
        Debug.Log("We are here");
        GameObject val;
        map.TryGetValue(name, out val);
        Debug.Log("We found it!");
        return val;
    }
}
