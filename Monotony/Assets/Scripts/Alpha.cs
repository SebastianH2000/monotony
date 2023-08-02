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
        Color parentColor = gameObject.GetComponent<SpriteRenderer>().color;
        parentColor.a = alpha;
        gameObject.GetComponent<SpriteRenderer>().color = parentColor;

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
    }
}
