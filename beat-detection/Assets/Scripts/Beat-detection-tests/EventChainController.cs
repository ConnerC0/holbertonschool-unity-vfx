using UnityEngine;
using UnityEngine.Events;

public class EventChainController : MonoBehaviour
{
    [SerializeField] private int tickCount = 10;
    [SerializeField] private UnityEvent onChainCompleted;
    [SerializeField] private GameEvent triggerEvent;

    private ChainListener[] chainListeners;

    private void Start()
    {
        InitializeChainListeners();
        ConfigureChainLinks();
    }

    private void InitializeChainListeners()
    {
        chainListeners = new ChainListener[tickCount];

        for (int i = 0; i < tickCount; i++)
        {
            var listenerObj = new GameObject($"ChainListener_{i}");
            var listener = listenerObj.AddComponent<ChainListener>();
            listener.Initialize(triggerEvent, isActive: i == 0);

            chainListeners[i] = listener;
        }
    }

    private void ConfigureChainLinks()
    {
        for (int i = 0; i < tickCount; i++)
        {
            if (i < tickCount - 1)
            {
                int nextIndex = i + 1;
                chainListeners[i].OnTriggered += () => chainListeners[nextIndex].Activate();
            }
            else
            {
                chainListeners[i].OnTriggered += onChainCompleted.Invoke;
            }

            var i1 = i;
            chainListeners[i].OnTriggered += () => chainListeners[i1].Deactivate();
        }
    }

    [ContextMenu("Trigger Event Chain")]
    private void TriggerEventChain()
    {
        triggerEvent.TriggerEvent();
    }
}