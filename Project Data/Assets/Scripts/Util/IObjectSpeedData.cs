using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides a means of sampling true values for velocity & acceleration.
/// </summary>
public interface IObjectSpeedData
{
    /// <summary>
    /// Takes a sample of the object's current position & uses it for taking measurements.
    /// </summary>
    /// <param name="deltaTime">the time that has passed.</param>
    void SampleData(float deltaTime);

    /// <summary>
    /// Returns true if there is enough data to measure the object's speed.
    /// </summary>
    /// <returns>true if there are at least two position samples taken.</returns>
    bool VelocityDataExists();

    /// <summary>
    /// Gets the measured velocity of this object.
    /// </summary>
    /// <returns>the measured velocity, or 0 if there is none.</returns>
    Vector3 GetVelocity();
}
