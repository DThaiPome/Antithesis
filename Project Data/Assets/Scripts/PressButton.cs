/**using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class PressButton : XRBaseInteractable
{
    private double yMin = 0.0;
    private double yMax = 0.0;

    private double prevHandHeight = 0.0;
    private XRBaseInteractor hoverInteractor = null;

    protected override void Awake()
    {
        base.Awake();
        onHoverEnter.AddListener(StartPress);
        onHoverExit.AddListener(EndPress);
    }

    private void OnDestroy()
    {
        onHoverEnter.RemoveListener(StartPress);
        onHoverExit.RemoveListener(EndPress);
    }

    private void StartPress(XRBaseInteractor interactor)
    {
        hoverInteractor = interactor;
        prevHandHeight = GetLocalYPosition(hoverInteractor.transform.position);
    }
    
    private void EndPress(XRBaseInteractor interactor)
    {
        hoverInteractor = null;
        prevHandHeight = 0.0;
    }

    private void Start()
    {
        SetMinMax();
    }

    private void SetMinMax()
    {
        Collider c = GetComponent<Collider>();
        yMin = transform.localPosition.y - (GetComponent<Collider>().bounds.size.y * 0.5);
        yMax = transform.localPosition.y;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
    }

    private double GetLocalYPosition(Vector3 position)
    {
        Vector3 localPosition = transform.root.InverseTransformPoint(position);

        return localPosition.y;
    }

    private void SetYPosition(float position)
    {
        Vector3 newPosn = transform.localPosition;
        newPosn.y = Mathf.Clamp(position, yMin, yMax);
        transform.localPosition = newPosition;
    }

    private void CheckPress()
    {

    }

    private bool InPosition()
    {
        return false;
    }
}**/
