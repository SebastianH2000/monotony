using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RyansNamespace
{
    [RequireComponent(typeof(AudioSource))]
    public class SFXManager : MonoBehaviour
    {
        [SerializeField] private AudioSource regularAS;
        [SerializeField] private AudioSource loopAS;

        [SerializeField] private AudioClip[] sfxClips;
        private Dictionary<string, AudioClip> sfxDict = new Dictionary<string, AudioClip>();

        private void Awake() {
            regularAS = GetComponent<AudioSource>();

            for (int i = 0; i < sfxClips.Length; i++)
                sfxDict.Add(sfxClips[i].name, sfxClips[i]);
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void PlaySFX(string sfxName, float volume = 1.0f, bool loop = false) {
            if (sfxDict.ContainsKey(sfxName)) {
                if (loop) {
                    loopAS.clip = sfxDict[sfxName];
                    loopAS.volume = volume;
                    loopAS.Play();
                } else {
                    regularAS.PlayOneShot(sfxDict[sfxName], volume);
                }
            }
        }
    }
}
