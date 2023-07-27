using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris: MonoBehaviour
{
    // Start is called before the first frame update

    public float maxDist = 0.75f;
    public float viewRange = 5;
    public Vector2 lockPos = new Vector2(0,0);
    public Rigidbody2D rb;
    public Vector2 parentPos;
    public Vector2 mousePos;
    public Vector2 maxPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //Color tempColor = Color.HSVToRGB(Random.value, 1, 1);
        //tempColor.a = 1f;
        GetComponent<SpriteRenderer>().color = Color.HSVToRGB(Random.value, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        maxDist = transform.parent.localScale.x / 4;
        viewRange = 6 / transform.parent.localScale.x;

        parentPos = transform.parent.position;
        Vector3 parentPos3 = new Vector3(parentPos.x,parentPos.y,0);
        float mouseDist = Vector2.Distance(parentPos, (Camera.main.ScreenToWorldPoint(Input.mousePosition))) /viewRange;
        mouseDist = Mathf.Clamp(mouseDist, 0, 1);
        Vector2 maxPosition = Vector2.ClampMagnitude((Camera.main.ScreenToWorldPoint(Input.mousePosition) - parentPos3), maxDist);
        rb.position = (Vector2.Lerp(new Vector2(0,0), maxPosition, mouseDist)+parentPos);
    }
}
