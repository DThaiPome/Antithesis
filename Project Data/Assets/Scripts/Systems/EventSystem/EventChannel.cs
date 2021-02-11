using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class that represents one event channel 

// need to create an event channel object to attatch script to
public class EventChannel : MonoBehaviour {

    public static EventChannel current;

    private void Awake()
    {
        current = this;
    }

    public event Action<int> onDamagePlayerEvent;

    public event Action<GameObject, int> onAttackObjectEvent;

    public event Action<IStatBuff> onAddBuffToPlayerEvent;

    public event Action<IPlayerKick> onPlayerKickEvent;

    public event Action onCollectRitualComponentEvent;

    public event Action<GameObject> spawnWraithFogEvent;

    public event Action<string> onEnemyDestroyedEvent;

    public event Action<float> onRitualTimerUpdateEvent;
    
    //Subtracts the given health from the player's stats. Input is forced to be >= 0.
    public void onDamagePlayer(int damage)
    {
        if (onDamagePlayerEvent != null)
        {
            onDamagePlayerEvent(Mathf.Max(0, damage));
        }
    }

    public void onAttackObject(GameObject obj, int damage)
    {
        if (onAttackObjectEvent != null)
        {
            onAttackObjectEvent(obj, damage);
        }
    }

    public void onAddBuffToPlayer(IStatBuff buff)
    {
        if (onAddBuffToPlayerEvent != null)
        {
            onAddBuffToPlayerEvent(buff);
        }
    }

    public void onPlayerKick(IPlayerKick kickData)
    {
        if (onPlayerKickEvent != null)
        {
            onPlayerKickEvent(kickData);
        }
    }

    public void onCollectRitualComponent()
    {
        if (onCollectRitualComponentEvent != null)
        {
            onCollectRitualComponentEvent();
        }
    }

    public void spawnWraithFog(GameObject fog)
    {
        if (spawnWraithFogEvent != null)
        {
            spawnWraithFogEvent(fog);
        }
    }

    public void onEnemyDestroyed(string tag)
    {
        if (onEnemyDestroyedEvent != null)
        {
            onEnemyDestroyedEvent(tag);
        }
    }

    public void onRitualTimerUpdate(float time)
    {
        if (onRitualTimerUpdateEvent != null)
        {
            onRitualTimerUpdateEvent(time);
        }
    }
}
