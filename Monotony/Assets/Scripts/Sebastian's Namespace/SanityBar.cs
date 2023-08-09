using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            if (!(SceneManager.GetActiveScene().name == "GroceryScene")) {
                this.transform.localScale = new Vector3(SavePlayerData.sanity,1,1);
                this.transform.localPosition = new Vector3(SavePlayerData.sanity*1.35f-0.51f,-0.015f,this.transform.localPosition.z);
            }
            else {
                this.transform.localScale = new Vector3(SavePlayerData.sanity*1.2f,1.2f,1);
                this.transform.localPosition = new Vector3(SavePlayerData.sanity*1.5f-1.55f,0.69f,this.transform.localPosition.z);
            }
        }
    }
}
