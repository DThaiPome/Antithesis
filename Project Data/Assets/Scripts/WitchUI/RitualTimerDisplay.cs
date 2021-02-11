using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RitualTimerDisplay : MonoBehaviour
{
    [SerializeField]
    private Text timerText;

    void Start()
    {
        EventChannel.current.onRitualTimerUpdateEvent += this.OnTimerUpdate;
        this.OnTimerUpdate(0);
    }

    void OnDestroy()
    {
        EventChannel.current.onRitualTimerUpdateEvent -= this.OnTimerUpdate;
    }

    private void OnTimerUpdate(float time)
    {
        if (time > 0)
        {
            this.gameObject.SetActive(true);
            this.timerText.text = time.ToString("F1");
        } else
        {
            this.gameObject.SetActive(false);
            this.timerText.text = "";
        }
    }
}
