using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlimitedItemSpawner : ARandomItemDistributor
{
    [Serializable]
    ///Weights of a set of items will be summed, then normalized. Just don't make it negative.
    private struct ItemProbability 
    {
        public GameObject prefab; 
        public float weight;

        public ItemProbability(GameObject prefab, float weight)
        {
            this.prefab = prefab;
            this.weight = weight;
        }
    }

    [SerializeField]
    private List<ItemProbability> itemsToSpawn = new List<ItemProbability>();
    private List<ItemProbability> items;
    [SerializeField]
    private List<Transform> spawnLocations = new List<Transform>();

    protected override void BeforeStart()
    {
        base.BeforeStart();
        this.items = InitItems(this.itemsToSpawn);
    }

    /// <summary>
    /// Initialize the normalized list of items so that it is ready to be picked from.
    /// </summary>
    /// <param name="items">The original list of items</param>
    /// <returns>The initialized list of items</returns>
    private List<ItemProbability> InitItems(List<ItemProbability> items)
    {
        List<ItemProbability> result = new List<ItemProbability>();

        List<float> weights = GetAllWeights(items);

        weights = this.StackWeights(this.NormalizeWeights(weights));

        for(int i = 0; i < items.Count; i++)
        {
            result.Add(new ItemProbability(items[i].prefab, weights[i]));
        }

        return result;
    }

    /// <summary>
    /// Spawn one randomly selected item to each location in the list of locations.
    /// </summary>
    public override void DistributeItems()
    {
        Queue<Transform> locations = new Queue<Transform>(this.spawnLocations);
        
        while(locations.Count > 0)
        {
            this.SpawnRandomItemHere(locations.Dequeue());
        }
    }

    /// <summary>
    /// Picks a random item from the list of items and spawns it at this location. The spawned
    /// object retains the transform's rotation and position.
    /// </summary>
    /// <param name="location">the location to spawn an item</param>
    /// <returns>the spawned object</returns>
    private GameObject SpawnRandomItemHere(Transform location)
    {
        int randIndex = this.PickRandomFromStackedWeights(GetAllWeights(this.items));
        if (randIndex == -1) {
            return null;
        }
        GameObject item = this.items[randIndex].prefab;
        if (item != null)
        {
            GameObject g = Instantiate(item, location.position, location.rotation);
            return g;
        }
        return null;
    }

    /// <summary>
    /// Returns a list of the weights of each item in the list, in order.
    /// </summary>
    /// <param name="items">a list of items</param>
    /// <returns>the respective list of weights</returns>
    private static List<float> GetAllWeights(List<ItemProbability> items)
    {
        List<float> result = new List<float>();
        foreach(ItemProbability item in items)
        {
            result.Add(item.weight);
        }
        return result;
    }
}
