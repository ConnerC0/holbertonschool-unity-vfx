using UnityEngine;
using UnityEngine.Events;

public class BeatDetection : MonoBehaviour
{
    public float[] parametricSampleBars; // Assuming you have an array of eight parametric sample bars
    public float[] thresholdVolumes; // Array of threshold volumes for event triggering
    private bool[] eventTriggered; // Array of flags to keep track of whether an event has been triggered or not

    [System.Serializable]
    public class VolumeReachedEvent : UnityEvent<int> { }
    public VolumeReachedEvent[] volumeReachedEvents; // Unity Events corresponding to each sample bar
    public GameEvent[] VolumeEvents;

    void Start()
    {
        // Initialize the parametric sample bars array
        parametricSampleBars = AudioPeer._freqBand;
        thresholdVolumes = new float[parametricSampleBars.Length];
        thresholdVolumes[0] = .2f;
        thresholdVolumes[1] = 1f;
        thresholdVolumes[2] = 1f;
        thresholdVolumes[3] = 3f;
        thresholdVolumes[4] = 2f;
        thresholdVolumes[5] = 4f;
        thresholdVolumes[6] = 1f;
        thresholdVolumes[7] = 6f;
        eventTriggered = new bool[parametricSampleBars.Length];
        for (int i = 0; i < eventTriggered.Length; i++)
        {
            eventTriggered[i] = false;
        }
        volumeReachedEvents = new VolumeReachedEvent[parametricSampleBars.Length];
        for (int i = 0; i < volumeReachedEvents.Length; i++)
        {
            volumeReachedEvents[i] = new VolumeReachedEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < parametricSampleBars.Length; i++)
        {
            if (parametricSampleBars[i] >= thresholdVolumes[i] && !eventTriggered[i])
            {
                // Trigger event when the sample bar reaches the specified volume
                TriggerEvent(i);
                eventTriggered[i] = true;
            }
            else if (parametricSampleBars[i] < thresholdVolumes[i] && eventTriggered[i])
            {
                // Reset flag when the sample bar goes back below the threshold volume
                eventTriggered[i] = false;
            }
        }
    }

    void TriggerEvent(int index)
    {
        // Invoke the UnityEvent for the specified sample bar index
        VolumeEvents[index].TriggerEvent();
    }
}
