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
        else if (this.name == "Pop-up Exit Button") {
            GameObject.Find("Email").GetComponent<Emails>().closePopupAudio.Play();
            Destroy(this.transform.parent.gameObject);
        }
    }

    void OnMouseEnter() {
        this.GetComponent<SpriteRenderer>().color = new Color(0.8f,0.8f,0.8f,1);

        if (this.name == "Pop-up Exit Button") {
            this.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    void OnMouseExit() {
        if (this.name != "Pop-up Exit Button") {
            this.GetComponent<SpriteRenderer>().color = new Color (1,1,1,1);
        }

        if (this.name == "Pop-up Exit Button") {
            this.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
