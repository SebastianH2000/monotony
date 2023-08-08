using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using SebastiansNamespace;

public class pp_control : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("For Testing")]
    [SerializeField]
    bool lookingAtMonster;

    [SerializeField]
    [Range(0, 100)]
    float sanityPercent;

    [Header("PP values (static)")]
    [SerializeField]
    [Range(0, (float)0.5)]
    float vignetteIntensity;

    [SerializeField]
    [Range(0, 2)]
    float postExposureVal;

    private Volume volume;
    private Vignette vignette;
    private ColorAdjustments colorAdjustments;
    private FilmGrain filmgrain;

    void Start()
    {
        volume = GetComponent<Volume>();
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        vignetteIntensity = Mathf.Lerp(0.1f,0.3f,SavePlayerData.monsterDistance/4);
        //overrides
        sanityPercent = SavePlayerData.sanity*100;
        lookingAtMonster = SavePlayerData.lookingAtMonster;

        if (lookingAtMonster && volume.profile.TryGet<Vignette>(out vignette))
        {
            vignette.intensity.value = vignetteIntensity;
        }
        else if(volume.profile.TryGet<Vignette>(out vignette))
        {
            vignette.intensity.value = 0;
        }

        if (volume.profile.TryGet<ColorAdjustments>(out colorAdjustments) && volume.profile.TryGet<FilmGrain>(out filmgrain))
        {
            colorAdjustments.postExposure.value = (float)((100 - sanityPercent) * 0.01 * -1 * postExposureVal);
            filmgrain.intensity.value = (float)((100 - sanityPercent) * 0.01);
            colorAdjustments.saturation.value = -1 * (100-sanityPercent);
        }
        
    }
}
