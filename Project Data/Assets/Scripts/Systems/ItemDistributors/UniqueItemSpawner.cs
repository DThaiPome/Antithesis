using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueItemSpawner : ARandomItemDistributor
{
    [Serializable]
    private struct LocationProbability
    {
        public Transform location;
        public float weight;

        public LocationProbability(Transform location, float weight)
        {
            this.location = location;
            this.weight = weight;
        }
    }
    
    [SerializeField]
    private bool instantiateNewCopyOfEachItem = true;
    [SerializeField]
    private List<GameObject> itemsToSpawn;
    [SerializeField]
    private List<LocationProbability> locationsToSpawn;
    private List<LocationProbability> locations;

    protected override void BeforeStart()
    {
        base.BeforeStart();
        this.locations = this.InitLocations(this.locationsToSpawn);
    }

    /// <summary>
    /// Initializes the normalized list of locations so that it is ready to be picked from.
    /// </summary>
    /// <param name="locations">The original list of locations</param>
    /// <returns>The initialized list of locations</returns>
    private List<LocationProbability> InitLocations(List<LocationProbability> locations)
    {
        List<LocationProbability> result = new List<LocationProbability>();

        List<float> weights = this.InitWeights(GetWeights(locations));
        for(int i = 0; i < locations.Count; i++)
        {
            result.Add(new LocationProbability(locations[i].location, weights[i]));
        }

        return result;
    }

    public override void DistributeItems()
    {
        Queue<GameObject> prefabs = new Queue<GameObject>(ShuffleList<GameObject>(this.itemsToSpawn));
        List<LocationProbability> locations = new List<LocationProbability>(this.locations);
        while(prefabs.Count > 0 && locations.Count > 0)
        {
            GameObject g = prefabs.Dequeue();
            SpawnItemAtRandomLocation(g, locations);
            locations = this.InitLocations(locations);
        }
    }

    /// <summary>
    /// Randomizes the order of this list
    /// </summary>
    /// <typeparam name="T">the type of the list</typeparam>
    /// <param name="list">the list to shuffle</param>
    /// <returns>a shuffled list</returns>
    private static List<T> ShuffleList<T>(List<T> list)
    {
        List<T> result = new List<T>(list);

        for(int i = 0; i < result.Count; i++)
        {
            int rng = UnityEngine.Random.Range(0, result.Count);
            T t = result[i];
            result[i] = result[rng];
            result[rng] = t;
        }

        return result;
    }

    /// <summary>
    /// Picks a location from the list of locations, removes it, and spawns the item at that
    /// location.
    /// </summary>
    /// <param name="item">item to spawn</param>
    /// <param name="locations">pool of locations to spawn the item at</param>
    /// <returns>the spawned object</returns>
    private GameObject SpawnItemAtRandomLocation(GameObject item, List<LocationProbability> locations)
    {
        int randIndex = PickRandomFromStackedWeights(GetWeights(locations));
        if (randIndex == -1)
        {
            return null;
        }
        Transform location = locations[randIndex].location;
        locations.RemoveAt(randIndex);
        if (location != null)
        {
            if (this.instantiateNewCopyOfEachItem)
            {
                GameObject g = Instantiate(item, location.position, location.rotation);
                return g;
            } else
            {
                item.transform.position = location.position;
                item.transform.rotation = location.rotation;
                return item;
            }
        }
        return null;
    }

    /// <summary>
    /// Get a list of each weight from the locations, in order.
    /// </summary>
    /// <param name="locations">a list of locations</param>
    /// <returns>the respective list of weights</returns>
    private List<float> GetWeights(List<LocationProbability> locations)
    {
        List<float> result = new List<float>();
        
        foreach(LocationProbability location in locations)
        {
            result.Add(location.weight);
        }

        return result;
    }

    private List<float> InitWeights(List<float> weights)
    {
        return this.StackWeights(this.NormalizeWeights(weights));
    }
}
