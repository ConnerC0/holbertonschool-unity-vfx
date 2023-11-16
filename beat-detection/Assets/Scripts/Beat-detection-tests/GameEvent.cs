using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ChainEvent", menuName = "ScriptableObjects/ChainEvent", order = 1)]
public class GameEvent : ScriptableObject
{
    private List<IChainListener> subscribers = new List<IChainListener>();

    public void Subscribe(IChainListener action)
    {
        if (!subscribers.Contains(action))
        {
            subscribers.Add(action);
        }
    }

    public void Unsubscribe(IChainListener action)
    {
        if (subscribers.Contains(action))
        {
            subscribers.Remove(action);
        }
    }

    public void TriggerEvent()
    {
        Debug.Log("Triggering Response");
        var tempList = new List<IChainListener>(subscribers);
        foreach (var subscriber in tempList)
        {
            subscriber.OnEventRaised();
        }
    }
}