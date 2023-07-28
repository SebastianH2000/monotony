using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    public Rigidbody2D selectedObject;
    Vector3 offset;
    Vector3 mousePosition;
    public float maxSpeed = 10;
    Vector2 mouseForce;
    Vector3 lastPosition;
    public string beanState = "unclicked";
    private float clickerTimer = 0;
    private float codeTimer = 0;
    public float glitchValue = 5f;
    private float noiseTimer = 0;
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject)
            {
                selectedObject = targetObject.transform.gameObject.GetComponent<Rigidbody2D>();
                offset = selectedObject.transform.position - mousePosition;
            }

            if (beanState == "unclicked")
            {
                beanState = "codeDisplayOpen"; GetComponent<SpriteRenderer>().color = Color.HSVToRGB(1, 0, 1);
            }
            else if (beanState == "codeDisplayOpen")
            {
                beanState = "codeDisplayClosed";
            }
            else if (beanState == "codeDisplayClosed")
            {
                beanState = "draggable";
            }
        }
        if (beanState == "draggable")
        {
            if (selectedObject)
            {
                mouseForce = (mousePosition - lastPosition) / Time.deltaTime;
                mouseForce = Vector2.ClampMagnitude(mouseForce, maxSpeed);
                lastPosition = mousePosition;
            }
            if (Input.GetMouseButtonUp(0) && selectedObject)
            {
                selectedObject.velocity = Vector2.zero;
                selectedObject.AddForce(mouseForce, ForceMode2D.Impulse);
                selectedObject = null;
            }
        }
        else if (beanState == "unclicked")
        {
            clickerTimer += Time.deltaTime;
            float lightness = (Mathf.Sin(clickerTimer*6)+3)/4;
            GetComponent<SpriteRenderer>().color = Color.HSVToRGB(1, 0, lightness);
        }
        else if (beanState == "codeDisplayOpen")
        {
            codeTimer = Mathf.Clamp(codeTimer + Time.deltaTime, 0, 1);
            GameObject codeDisplayReal = GameObject.Find("Bean Code Real");
            GameObject codeDisplayOrigin = GameObject.Find("Bean Code Origin");
            GameObject codeDisplayTarget = GameObject.Find("Bean Code Target");

            codeDisplayReal.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
            codeDisplayReal.transform.localScale = Vector3.Lerp(codeDisplayOrigin.transform.localScale, codeDisplayTarget.transform.localScale,codeTimer);
            codeDisplayReal.transform.localPosition = Vector3.Lerp(codeDisplayOrigin.transform.localPosition, codeDisplayTarget.transform.localPosition, codeTimer);
        }
        else if (beanState == "codeDisplayClosed")
        {
            codeTimer = Mathf.Clamp(codeTimer - Time.deltaTime, 0, 1);
            GameObject codeDisplayReal = GameObject.Find("Bean Code Real");
            GameObject codeDisplayOrigin = GameObject.Find("Bean Code Origin");
            GameObject codeDisplayTarget = GameObject.Find("Bean Code Target");

            if (codeTimer < 0.0001) codeDisplayReal.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            codeDisplayReal.transform.localScale = Vector3.Lerp(codeDisplayOrigin.transform.localScale, codeDisplayTarget.transform.localScale, codeTimer);
            codeDisplayReal.transform.localPosition = Vector3.Lerp(codeDisplayOrigin.transform.localPosition, codeDisplayTarget.transform.localPosition, codeTimer);
        }




        noiseTimer += Time.deltaTime;
        glitchValue = Mathf.Clamp((glitchValue+((Mathf.PerlinNoise(noiseTimer,noiseTimer)-0.55f)/80)),0,5);
        GameObject monster = GameObject.Find("monster-frame");
        GameObject customer = GameObject.Find("character-no-eyes");
        if (glitchValue < 0.3f)
        {
            GameObject.Find("Background Music").GetComponent<AudioSource>().pitch = 0.25f;
            customer.GetComponent<Alpha>().setAlpha(0);
            monster.GetComponent<Alpha>().setAlpha(1);

        }
        else
        {
            GameObject.Find("Background Music").GetComponent<AudioSource>().pitch = 1;
            customer.GetComponent<Alpha>().setAlpha(1);
            monster.GetComponent<Alpha>().setAlpha(0);
        }
    }
    void FixedUpdate()
    {
        if (beanState == "draggable")
        {
            if (selectedObject)
            {
                selectedObject.MovePosition(mousePosition + offset);
            }
        }
    }
}
