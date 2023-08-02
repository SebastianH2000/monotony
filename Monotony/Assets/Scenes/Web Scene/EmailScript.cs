using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SebastiansNamespace;

public class EmailScript : MonoBehaviour
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
        this.transform.Find("Sprites").transform.Find("Background").gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5f,0.5f,0.5f,1);
    }

    void OnMouseExit() 
    {
        if (GameObject.Find("Email").GetComponent<Emails>().currentEmailClicked != int.Parse(this.name)) {
            this.transform.Find("Sprites").transform.Find("Background").gameObject.GetComponent<SpriteRenderer>().color = new Color(0.71f,0.71f,0.71f,1);
        }
    }

    void OnMouseDown() 
    {
        SpriteRenderer[] children = this.transform.parent.gameObject.GetComponentsInChildren<SpriteRenderer>();
        Color newColor = new Color(0.71f,0.71f,0.71f,1);
        foreach (SpriteRenderer child in children)
        {
            if (child.gameObject.transform.tag == "EmailBackground") {
                child.color = newColor;
            }
        }
        //this.transform.parent.transform.Find("Sprites").transform.Find("Background")
        this.transform.Find("Sprites").transform.Find("Background").gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5f,0.5f,0.5f,1);
        //Debug.Log(this.name);
        GameObject.Find("Email").GetComponent<Emails>().emailClicked(int.Parse(this.name));
    }
}
