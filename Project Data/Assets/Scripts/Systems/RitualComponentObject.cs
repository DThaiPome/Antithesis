using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitualComponentObject : MonoBehaviour
{
    public void OnCollect()
    {
        if (this.gameObject.activeSelf)
        {
            EventChannel.current.onCollectRitualComponent();
            this.gameObject.SetActive(false);
        }
    }
}
