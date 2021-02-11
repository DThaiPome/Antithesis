using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitualComponentTracker : MonoBehaviour
{
    [SerializeField]
    private int countToWin = 5;
    [SerializeField]
    private GameObject winTrigger;

    private int count;



    void Start()
    {
        EventChannel.current.onCollectRitualComponentEvent += this.OnRitualComponentGet;
        this.winTrigger.SetActive(false);
    }

    void OnDestroy()
    {
        EventChannel.current.onCollectRitualComponentEvent += this.OnRitualComponentGet;
    }

    private void OnRitualComponentGet()
    {
        this.count++;
        if (this.count >= this.countToWin)
        {
            this.winTrigger.SetActive(true);
        }
    }
}
