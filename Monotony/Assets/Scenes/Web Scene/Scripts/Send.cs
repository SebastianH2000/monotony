using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SebastiansNamespace;

public class Send : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        if (GameObject.Find("Email").GetComponent<Emails>().canSend) {
            this.GetComponent<SpriteRenderer>().color = new Color(0.65f,0.65f,0.65f,1);
        }
    }

    void OnMouseExit() 
    {
        if (GameObject.Find("Email").GetComponent<Emails>().canSend) {
            this.GetComponent<SpriteRenderer>().color = new Color(0.85f,0.85f,0.85f,1);
        }
        else {
            this.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1);
        }
    }

    void OnMouseDown() 
    {
        GameObject.Find("Email").GetComponent<Emails>().emailSent();
    }
}
