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
            this.GetComponent<SpriteRenderer>().color = new Color(0.45f,0.45f,0.45f,1);
        }
    }

    void OnMouseExit() 
    {
        if (GameObject.Find("Email").GetComponent<Emails>().canSend) {
            this.GetComponent<SpriteRenderer>().color = new Color(0.4f,0.4f,0.4f,1);
        }
        else {
            this.GetComponent<SpriteRenderer>().color = new Color(0.35f,0.35f,0.35f,1);
        }
    }

    void OnMouseDown() 
    {
        GameObject.Find("Email").GetComponent<Emails>().emailSent();
    }
}
