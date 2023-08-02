using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Alpha : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setAlpha(float alpha)
    {
        //parent
        if (gameObject.GetComponent<SpriteRenderer>()) {
            Color parentColor = gameObject.GetComponent<SpriteRenderer>().color;
            parentColor.a = alpha;
            gameObject.GetComponent<SpriteRenderer>().color = parentColor;
        }

        //children
        SpriteRenderer[] children = GetComponentsInChildren<SpriteRenderer>();
        Color newColor;
        foreach (SpriteRenderer child in children)
        {
            newColor = child.color;
            newColor.a = alpha;
            child.color = newColor;
        }

        TextMeshProUGUI[] TMchildren = GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI child in TMchildren)
        {
            newColor = child.color;
            newColor.a = alpha;
            child.color = newColor;
        }

        if (alpha == 0) {
            BoxCollider2D[] Boxchildren = GetComponentsInChildren<BoxCollider2D>();
            foreach (BoxCollider2D child in Boxchildren)
            {
                child.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
        else {
            BoxCollider2D[] Boxchildren = GetComponentsInChildren<BoxCollider2D>();
            foreach (BoxCollider2D child in Boxchildren)
            {
                child.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
}
