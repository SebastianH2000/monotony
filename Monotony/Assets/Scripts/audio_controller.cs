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
    int lowPassNormalValue;
    [Range(0, 22000)]
    [SerializeField]
    int lowPassFilteredValue;
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
    private float lerpValue;

    void Start()
    {
        wasLooking = false;
        curNoise =  -80;
    }

    // Update is called once per frame
    void Update()
    {
        /*isLookingAtMonster = SavePlayerData.lookingAtMonster;
        if (!wasLooking && isLookingAtMonster)
        {
            
            audioMixer.GetFloat(lowpass, out curLowpass);
            StartCoroutine(LerpMixerValue(curLowpass, lowPassFilteredValue, lowpass, isLookingAtMonster));

            audioMixer.GetFloat(noiseVol, out curNoise);
            StartCoroutine(LerpMixerValue(curNoise, maxNoiseVol, noiseVol, isLookingAtMonster));

            wasLooking = true;
        }
        if(wasLooking && !isLookingAtMonster)
        {
            audioMixer.GetFloat(lowpass, out curLowpass);
            StartCoroutine(LerpMixerValue(curLowpass, lowPassNormalValue, lowpass, !isLookingAtMonster));

            audioMixer.GetFloat(noiseVol, out curNoise);
            StartCoroutine(LerpMixerValue(curNoise, -80, noiseVol, isLookingAtMonster));

            wasLooking = false;
        }*/

        isLookingAtMonster = SavePlayerData.lookingAtMonster;
        float increment = 1 / transitionDuration;
        int isLookingMult = 0;
        if (SavePlayerData.lookingAtMonster) {
            isLookingMult = 1;
        }
        else {
            isLookingMult = -1;
        }
        lerpValue += increment * Time.deltaTime * isLookingMult * Mathf.Clamp(SavePlayerData.monsterDistance,1,4);
        lerpValue = Mathf.Clamp(lerpValue, 0, 1);
        audioMixer.SetFloat("noiseVol", Mathf.Lerp(-80, maxNoiseVol, lerpValue));
        //Debug.Log(lerpValue);
        audioMixer.SetFloat("lowpass", Mathf.Lerp(lowPassFilteredValue, lowPassNormalValue, lerpValue));
    }

    IEnumerator LerpMixerValue(float startValue, float endValue, bool looking)
    {
        float increment = 1 / transitionDuration;
        yield return 0;

        /*while (lerpValue < 1 || looking != isLookingAtMonster)
        {
            lerpValue += increment * Time.deltaTime;
            audioMixer.SetFloat(name, Mathf.Lerp(startValue, endValue, lerpValue));
            yield return 0;
        }*/
        /*int isLookingMult = 0;
        if (looking) {
            isLookingMult = 1;
        }
        else {
            isLookingMult = -1;
        }
        lerpValue += increment * Time.deltaTime * isLookingMult;
        lerpValue = Mathf.Clamp(lerpValue, -1, 1);
        audioMixer.SetFloat("noiseVol", Mathf.Lerp(startValue, endValue, lerpValue));
        audioMixer.SetFloat("lowpass", Mathf.Lerp(startValue, endValue, lerpValue));
        yield return 0;*/
    }
}
