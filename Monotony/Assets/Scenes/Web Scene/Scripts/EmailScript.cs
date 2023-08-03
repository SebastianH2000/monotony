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
        float currentA  = this.transform.Find("Sprites").transform.Find("Background").gameObject.GetComponent<SpriteRenderer>().color.a;
        this.transform.Find("Sprites").transform.Find("Background").gameObject.GetComponent<SpriteRenderer>().color = new Color(0.7f,0.7f,0.7f,currentA);
    }

    void OnMouseExit() 
    {
        if (GameObject.Find("Email").GetComponent<Emails>().currentEmailClicked != int.Parse(this.name)) {
            float currentA  = this.transform.Find("Sprites").transform.Find("Background").gameObject.GetComponent<SpriteRenderer>().color.a;
            this.transform.Find("Sprites").transform.Find("Background").gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,currentA);
        }
    }

    void OnMouseDown() 
    {
        SpriteRenderer[] children = this.transform.parent.gameObject.GetComponentsInChildren<SpriteRenderer>();
        float currentA  = this.transform.Find("Sprites").transform.Find("Background").gameObject.GetComponent<SpriteRenderer>().color.a;
        Color newColor = new Color(1f,1f,1f,currentA);
        foreach (SpriteRenderer child in children)
        {
            if (child.gameObject.transform.tag == "EmailBackground") {
                child.color = newColor;
            }
        }
        //this.transform.parent.transform.Find("Sprites").transform.Find("Background")
        this.transform.Find("Sprites").transform.Find("Background").gameObject.GetComponent<SpriteRenderer>().color = new Color(0.7f,0.7f,0.7f,currentA);
        //Debug.Log(this.name);
        GameObject.Find("Email").GetComponent<Emails>().emailClicked(int.Parse(this.name));
    }
}
