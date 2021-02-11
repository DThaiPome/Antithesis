using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// Randomly distributes a set of items across a set of locations.
/// </summary>
public abstract class ARandomItemDistributor : MonoBehaviour
{
    [SerializeField]
    private bool distributeOnStart = true;

    /// <summary>
    /// If specified, distribute items now.
    /// </summary>
    void Start()
    {
        this.BeforeStart();
        if (this.distributeOnStart)
        {
            this.DistributeItems();
        }
    }

    /// <summary>
    /// Implement this to add more behaviour before stuff is distributed.
    /// </summary>
    protected virtual void BeforeStart() { }

    /// <summary>
    /// Implement this to add more behaviour after stuff is distributed.
    /// </summary>
    protected virtual void AfterStart() { }

    /// <summary>
    /// Distribute's this object's items randomly across a set of locations.
    /// </summary>
    public abstract void DistributeItems();

    /// <summary>
    /// Return a new list of weights normalized to be within [0, 1].
    /// </summary>
    /// <param name="weights">a list of weights</param>
    /// <returns>a new list of normalized weights</returns>
    protected List<float> NormalizeWeights(List<float> weights)
    {
        List<float> result = new List<float>();
        float sum = 0;
        foreach (float f in weights)
        {
            sum += f;
        }
        sum = sum == 0 ? 1 : sum;
        foreach (float f in weights)
        {
            result.Add(f / sum);
        }
        return result;
    }

    /// <summary>
    /// Returns a new list of weights, where each weight "w" in the new list is the sum of all weights
    /// in the old list within the range [0, w] (sum of all previous weights, including the current one).
    /// </summary>
    /// <param name="items">a list of weights</param>
    /// <returns>a list of stacked weights.</returns>
    protected List<float> StackWeights(List<float> weights)
    {
        List<float> result = new List<float>();

        float sum = 0;
        foreach (float f in weights)
        {
            float newWeight = f + sum;
            sum += f;
            result.Add(newWeight);
        }

        return result;
    }

    /// <summary>
    /// Picks a random index within [0, size of list), using the stacked weights as probabilities.
    /// </summary>
    /// <param name="weights">a list of stacked probability weights</param>
    /// <returns>a random index within the list</returns>
    protected int PickRandomFromStackedWeights(List<float> weights)
    {
        float rng = UnityEngine.Random.value;
        for (int i = 0; i < weights.Count; i++)
        {
            float weight = weights[i];
            if (weight != 0 && rng <= weight)
            {
                return i;
            }
        }
        return -1;
    }
}
