using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitualProcess : MonoBehaviour
{
    [SerializeField]
    private int componentsToWin = 4;
    [SerializeField]
    private float ritualDuration = 30;

    private int count;

    private bool started;
    private float timeLeftInRitual;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Win Item"))
        {
            this.count++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Win Item"))
        {
            this.count--;
        }
    }

    void Update()
    {
        if (!this.started && count >= this.componentsToWin)
        {
            this.RitualStarted();
        }
        if (this.started)
        {
            this.RitualUpdate();
        }
    }

    private void RitualStarted()
    {
        this.started = true;
        this.timeLeftInRitual = this.ritualDuration;
        GameManager.SetRitualActive();

        Debug.Log("Ritual Started!");
    }

    private void RitualUpdate()
    {
        this.timeLeftInRitual = Mathf.Max(this.timeLeftInRitual - Time.deltaTime, 0);
        EventChannel.current.onRitualTimerUpdate(this.timeLeftInRitual);
        if (this.timeLeftInRitual <= 0 && !GameManager.isGameLost())
        {
            this.RitualComplete();
        }
    }

    private void RitualComplete()
    {
        GameManager.SetRitualComplete();
        Debug.Log("Ritual Complete!");
        this.started = false;
        this.count = 0;
    }
}
