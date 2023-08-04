using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RyansNamespace {
    [RequireComponent(typeof(SFXManager))]
    public class AppManager : MonoBehaviour
    {
        public static AppManager instance { get; private set; }

        public SFXManager sfxManager { get; private set; }

        // Start is called before the first frame update
        void Start()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            sfxManager = GetComponent<SFXManager>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
