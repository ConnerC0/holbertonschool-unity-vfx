using System;
using UnityEngine;

public class ChainListener : MonoBehaviour, IChainListener
{
    public event Action OnTriggered;

    public GameEvent subscribedEvent;

    private void Start()
    {
        
        subscribedEvent.Subscribe(this);
    }

    public void Initialize(GameEvent eventTrigger, bool isActive)
    {
        subscribedEvent = eventTrigger;
        gameObject.SetActive(isActive);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        subscribedEvent?.Subscribe(this);
    }

    private void OnDisable()
    {
        subscribedEvent?.Unsubscribe(this);
    }

    public void OnEventRaised()
    {
        Debug.Log("Chain event triggered.");
        OnTriggered?.Invoke();
    }
}