using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RyansNamespace {
    public class SpawnEyes_Placed : MonoBehaviour
    {
        // Start is called before the first frame update
        public GameObject eyePrefab;
        void Start()
        {
            //float minSize = 1;
            //float maxSize = 3;
            //float eyeAmount = 50;
            //float sizeDiff = (maxSize - minSize)/eyeAmount;
            for (int i = 0; i < 50; i++)
            {
                //Instantiate(eyePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            }
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}

