using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpeedDataCollector : IObjectSpeedData
{
    private Transform objectTransform;
    private bool useLocal;

    private List<Vector3> sampleData;
    private int maxDataCount = 2;

    private Vector3 currentVelocity;

    public ObjectSpeedDataCollector(Transform objectTransform, bool useLocal = true)
    {
        this.objectTransform = objectTransform;
        this.useLocal = useLocal;

        this.sampleData = new List<Vector3>();
        this.currentVelocity = Vector3.zero;
    }

    public Vector3 GetVelocity()
    {
        return this.currentVelocity;
    }

    public void SampleData(float deltaTime)
    {
        this.UpdateList();
        if (this.PVelocityDataExists())
        {
            this.UpdateVelocity(deltaTime);
        }
    }

    /// <summary>
    /// Add a sample, and ensure the list stays within its size limit.
    /// </summary>
    private void UpdateList()
    {
        this.sampleData.Add(this.useLocal ? this.objectTransform.localPosition : this.objectTransform.position);
        while (this.sampleData.Count > this.maxDataCount)
        {
            this.sampleData.RemoveAt(0);
        }
    }

    private void UpdateVelocity(float deltaTime)
    {
        int size = this.sampleData.Count;
        Vector3 pos1 = this.sampleData[size - 1];
        Vector3 pos2 = this.sampleData[size - 2];
        Vector3 velocity = (pos1 - pos2) / deltaTime;
        this.currentVelocity = velocity;
    }

    public bool VelocityDataExists()
    {
        return this.PVelocityDataExists();
    }

    private bool PVelocityDataExists()
    {
        return sampleData.Count >= 2;
    }
}
