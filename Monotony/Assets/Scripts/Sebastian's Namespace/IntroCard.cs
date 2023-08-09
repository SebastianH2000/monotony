using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCard : MonoBehaviour
{
    public bool isShown = true;
    public GameObject exitButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter() {
        exitButton.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void OnMouseExit() {
        exitButton.GetComponent<SpriteRenderer>().enabled = false;
    }

    void OnMouseDown() {
        isShown = false;
        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<Alpha>().setAlpha(0f);
    }
}
