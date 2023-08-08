using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SebastiansNamespace {
    public class SanityBar : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            this.transform.localScale = new Vector3(SavePlayerData.sanity,1,1);
            this.transform.localPosition = new Vector3(SavePlayerData.sanity*1.35f-0.51f,-0.015f,this.transform.localPosition.z);
        }
    }
}
