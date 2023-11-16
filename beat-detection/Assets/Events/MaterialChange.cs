using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ChainListener))]
public class MaterialChange : MonoBehaviour
{
    private ChainListener chainListener;
    // Start is called before the first frame update
    void Start()
    {
        chainListener = GetComponent<ChainListener>();
        chainListener.OnTriggered += () => MaterialSwitch();
    }

    void MaterialSwitch(){
        
    }
}
