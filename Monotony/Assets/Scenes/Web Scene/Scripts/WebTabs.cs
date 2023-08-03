using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebTabs : MonoBehaviour
{
    private Rigidbody2D selectedObject;
    Vector3 offset;
    Vector3 mousePosition;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //mouse position in world space
        if (Input.GetMouseButtonDown(0)) //if left mouse button was clicked
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition); //get physics overlap
            if (targetObject) //if overlap hit anything
            {
                selectedObject = targetObject.transform.gameObject.GetComponent<Rigidbody2D>(); //set positions
                offset = selectedObject.transform.position - mousePosition; //set positions
                if (targetObject.name == gameObject.name) //if the overlap is hitting the object that this script is attached to
                {
                    /*if (gameObject.name == "EmailTab") //if this script is attached to the emailTab
                    {
                        GameObject.Find("Email").GetComponent<Alpha>().setAlpha(1f);
                        GameObject.Find("Excitement").GetComponent<Alpha>().setAlpha(0f);

                        GameObject.Find("EmailTab").GetComponent<SpriteRenderer>().color = new Color(0.7529413f, 0.7529413f, 0.7529413f, 1);
                        GameObject.Find("ExcitementTab").GetComponent<SpriteRenderer>().color = new Color(0.6037736f, 0.6037736f, 0.6037736f, 1);
                    }
                    else
                    {
                        GameObject.Find("Email").GetComponent<Alpha>().setAlpha(0f);
                        GameObject.Find("Excitement").GetComponent<Alpha>().setAlpha(1f);

                        GameObject.Find("EmailTab").GetComponent<SpriteRenderer>().color = new Color(0.6037736f, 0.6037736f, 0.6037736f, 1);
                        GameObject.Find("ExcitementTab").GetComponent<SpriteRenderer>().color = new Color(0.7529413f, 0.7529413f, 0.7529413f, 1);
                    }
                }
            }
        }*/
    }
}
