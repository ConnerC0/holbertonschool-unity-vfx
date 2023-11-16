using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListener : MonoBehaviour
{
    public GameObject[] gameObjectsToChange; // Array of GameObjects to change materials
    public Material[] materialsToCycleThrough; // Array of materials to cycle through
    public GameObject[] platforms; // Array of platforms to toggle

    private int currentMaterialIndex = 0; // Index of the current material in the materials array

    enum SampleBarIndex
    {
        Bar0,
        Bar1,
        Bar2,
        Bar3,
        Bar4,
        Bar5,
        Bar6,
        Bar7
    }

    void HandleVolumeReached(int sampleBarIndex)
{
    var targetIndex = InterpolateRange(sampleBarIndex, gameObjectsToChange.Length);
    ChangeGameObjectMaterial(targetIndex);
    targetIndex = InterpolateRange(sampleBarIndex, platforms.Length);    
    TriggerPlatform(platforms[targetIndex]);
}

    void ChangeGameObjectMaterial(int gameObjectIndex)
    {
        if (gameObjectIndex < gameObjectsToChange.Length)
        {
            Renderer renderer = gameObjectsToChange[gameObjectIndex].GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = materialsToCycleThrough[currentMaterialIndex];
            }

            currentMaterialIndex = (currentMaterialIndex + 1) % materialsToCycleThrough.Length;
        }
    }
    void TriggerPlatform(GameObject platform)
    {
        if (platform != null)
        {
            platform.SetActive(!platform.activeSelf);
        }
    }
    public static int InterpolateRange(int inputValue, int targetRangeMax)
    {
    const int sourceRangeMax = 7;

    // Normalize the input value to a range of 0 to 1
    float normalizedValue = (float)inputValue / sourceRangeMax;

    // Scale the normalized value to the target range and round to nearest integer
    int scaledValue = (int)Mathf.Round(normalizedValue * (targetRangeMax - 1));

    return scaledValue;
    }
}


