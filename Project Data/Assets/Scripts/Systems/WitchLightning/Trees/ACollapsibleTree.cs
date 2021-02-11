using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ACollapsibleTree : MonoBehaviour
{
    /// <summary>
    /// Set whether or not this tree is highlighted.
    /// </summary>
    /// <param name="isHighlighted">is the tree highlighted?</param>
    public abstract void SetHighlighted(bool isHighlighted);

    /// <summary>
    /// Knock down the tree.
    /// </summary>
    public abstract void KnockDown();

    /// <summary>
    /// Check if the tree has already been knocked down.
    /// </summary>
    /// <returns>returns true if the tree has been knocked down, false otherwise.</returns>
    public abstract bool IsKnockedDown();
}
