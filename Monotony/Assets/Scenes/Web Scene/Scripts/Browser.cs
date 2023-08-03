using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SebastiansNamespace;

public class Browser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown() {
        if (this.name == "Browser Icon") {
            GameObject.Find("Email").GetComponent<Emails>().browserOpen = true;
        }
        else if (this.name == "Exit Button") {
            GameObject.Find("Email").GetComponent<Emails>().browserOpen = false;
        }
        else {
            Debug.Log(this.name);
        }
    }
}
