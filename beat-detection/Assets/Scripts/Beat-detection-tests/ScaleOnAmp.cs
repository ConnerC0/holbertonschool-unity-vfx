using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnAmp : MonoBehaviour
{

    public float _startScale, _maxScale;
    public bool _useBuffer;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!_useBuffer){
            transform.localScale = new Vector3 ((AudioPeer._Amplitude * _maxScale) + _startScale,(AudioPeer._Amplitude * _maxScale) + _startScale,(AudioPeer._Amplitude * _maxScale) + _startScale);
        }
        if (_useBuffer){
            transform.localScale = new Vector3 ((AudioPeer._AmplitudeBuffer * _maxScale) + _startScale,(AudioPeer._AmplitudeBuffer * _maxScale) + _startScale,(AudioPeer._AmplitudeBuffer * _maxScale) + _startScale);
        }
    }
}
