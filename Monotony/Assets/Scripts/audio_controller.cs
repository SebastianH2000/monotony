using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using SebastiansNamespace;

public class audio_controller : MonoBehaviour
{
    [Header("audio stuff")]
    [SerializeField]
    AudioMixer audioMixer;
    [SerializeField]
    [Range(0, 22000)]
    int lowPassNormalValue = 15000;
    [Range(0, 22000)]
    [SerializeField]
    int lowPassFilteredValue = 500;
    [SerializeField]
    float transitionDuration;
    [SerializeField]
    [Range(0, 20)]
    float maxNoiseVol;


    [SerializeField]
    bool isLookingAtMonster;

    private bool wasLooking;

    const string lowpass = "lowpass";
    const string noiseVol = "noiseVol";
    private float curNoise;
    private float curLowpass;

    void Start()
    {
        wasLooking = false;
        curNoise =  -80;
    }

    // Update is called once per frame
    void Update()
    {
        if (!wasLooking && SavePlayerData.lookingAtMonster)
        {
            
            audioMixer.GetFloat(lowpass, out curLowpass);
            StartCoroutine(LerpMixerValue(curLowpass, lowPassFilteredValue, lowpass, isLookingAtMonster));

            audioMixer.GetFloat(noiseVol, out curNoise);
            StartCoroutine(LerpMixerValue(curNoise, maxNoiseVol, noiseVol, isLookingAtMonster));

            wasLooking = true;
        }
        if(wasLooking && !SavePlayerData.lookingAtMonster)
        {
            audioMixer.GetFloat(lowpass, out curLowpass);
            StartCoroutine(LerpMixerValue(curLowpass, lowPassNormalValue, lowpass, !isLookingAtMonster));

            audioMixer.GetFloat(noiseVol, out curNoise);
            StartCoroutine(LerpMixerValue(curNoise, -80, noiseVol, isLookingAtMonster));

            wasLooking = false;
        }
    }

    IEnumerator LerpMixerValue(float startValue, float endValue, string name, bool looking)
    {
        float lerpValue = 0;
        float increment = 1 / transitionDuration;

        while (lerpValue < 1 || looking != isLookingAtMonster)
        {
            lerpValue += increment * Time.deltaTime;
            audioMixer.SetFloat(name, Mathf.Lerp(startValue, endValue, lerpValue));
            yield return 0;
        }
    }
}
