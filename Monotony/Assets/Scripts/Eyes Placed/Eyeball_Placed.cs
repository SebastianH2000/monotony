using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyeball_Placed : MonoBehaviour
{
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.position = new Vector2(Random.Range(-5f,5f),Random.Range(-5f, 5f));
        //float transScale = Random.Range(1f,3f);
        //transform.localScale = new Vector3(transScale,transScale,1);
        //transform.position = new Vector3(rb.position.x, rb.position.y, 100-transScale);
        //GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transScale*1000);


        //setAlpha(0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        //setAlpha(0.5f);
    }

    public void setAlpha(float alpha)
    {
        SpriteRenderer[] children = GetComponentsInChildren<SpriteRenderer>();
        Color newColor;
        foreach (SpriteRenderer child in children)
        {
            newColor = child.color;
            newColor.a = alpha;
            child.color = newColor;
        }
    }
}
