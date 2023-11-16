using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class AudioPeer : MonoBehaviour
{

    AudioSource _audioSource;
    public static float[] _samples = new float[512];
    public static float[] _freqBand = new float[8];
    public static float[] _bandBuffer = new float[8];
    float[] _bufferDecrease = new float [8];
    float[] _freqBandHighest = new float[8];
    public static float[] _audioBand = new float[8];
    public static float[] _audioBandBuffer = new float[8];
    public static float _Amplitude, _AmplitudeBuffer;
    public static float _AmplitudeHighest;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource ();
        MakeFrequencyBands ();
        BandBuffer ();
        CreateAudioBands();
        GetAmplitude();
        Debug.Log("Amplitude: " + AudioPeer._AmplitudeHighest);
    }

    void GetSpectrumAudioSource(){
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    void MakeFrequencyBands(){
        /*
        * 22050 / 512 = 43 hertz per sample
        *
        * 20 - 60 hertz
            60 - 250 hertz
            250 - 500 hertz
            500 - 2000 hertz
            2000 - 4000 hertz
            4000 - 6000 hertz
            6000 - 20000 hertz
        *
        * Frequency Bands | # of Samples
        * 0 - 2 = 86 hertz
        * 1 - 4 =  172 hertz - range of 87-258 hertz
        * 2 - 8 = 344 hertz - range of 259-602 hertz
        * 3 - 16 = 688 hertz - range of 603-1290 hertz
        * 4 - 32 = 1376 hertz - range of 1291-2666 hertz
        * 5 - 64 = 2752 hertz - range of 2667-5418 hertz
        * 6 - 128 = 5504 hertz - range of 5419-10922 hertz
        * 7 - 256 = 11008 hertz - range of 10923-21930 hertz
        * 510 samples
        */

        int count = 0;

        for (int i = 0; i < 8; i++){
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2,i) * 2;

            if (i == 7){
                sampleCount += 2;
            }
            for(int j = 0; j < sampleCount; j++){
                average += _samples[count] * (count +1);
                    count++;
            }

            average /= count;

            _freqBand [i] = Mathf.Round((average * 10) * 1000f) / 1000f;
        }
    }

    void GetAmplitude(){
        float _CurrentAmplitude = 0;
        float _CurrentAmplitudeBuffer = 0;

        for (int i = 0; i < 8; i++){
            _CurrentAmplitude += _audioBand[i];
            _CurrentAmplitudeBuffer += _audioBandBuffer[i];
        }
        if (_CurrentAmplitude > _AmplitudeHighest){
            _AmplitudeHighest = _CurrentAmplitude;
        }
        _Amplitude = Mathf.Clamp01(_CurrentAmplitude / _AmplitudeHighest);
        _AmplitudeBuffer = _CurrentAmplitudeBuffer / _AmplitudeHighest;
    }

    void BandBuffer (){
        for (int g = 0; g < 8; g++){
            if (_freqBand[g] > _bandBuffer[g]){
                _bandBuffer[g] = _freqBand[g];
                _bufferDecrease [g] = 0.005f;
            }

            if (_freqBand [g] < _bandBuffer [g]){
                _bandBuffer[g] -= _bufferDecrease [g];
                _bufferDecrease[g] *= 1.2f;
            }
        }
    }

    void CreateAudioBands(){
        for (int i = 0; i<8;i++)
        {
            if (_freqBand[i] > _freqBandHighest[i]){
                _freqBandHighest[i] = _freqBand[i];
            }
            _audioBand[i] = (_freqBand[i]/_freqBandHighest[i]);
            _audioBandBuffer[i] = (_bandBuffer[i]/_freqBandHighest[i]);
        }
    }
}
