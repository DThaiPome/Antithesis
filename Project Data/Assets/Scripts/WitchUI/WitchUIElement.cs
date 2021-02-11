using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WitchUIElement : MonoBehaviour
{
    private bool isSelected;

    private EventTrigger eventTrigger;

    void Start()
    {
        this.eventTrigger = this.gameObject.AddComponent<EventTrigger>();
        this.AddEventTriggerEntry(EventTriggerType.PointerEnter, this.OnMouseEnter);
        this.AddEventTriggerEntry(EventTriggerType.PointerExit, this.OnMouseExit);
    }

    private void AddEventTriggerEntry(EventTriggerType type, Action action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = type;
        entry.callback.AddListener((eventData) => { action(); });
        eventTrigger.triggers.Add(entry);
    }

    public void OnMouseEnter()
    {
        this.isSelected = true;
    }

    public void OnMouseExit()
    {
        this.isSelected = false;
    }

    public bool IsSelected()
    {
        return this.isSelected;
    }
}
