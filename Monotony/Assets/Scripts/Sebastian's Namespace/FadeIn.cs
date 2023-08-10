using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SebastiansNamespace;

public class FadeIn : MonoBehaviour
{
    public bool isFading = true;
    private float fadeTimer = 0f;
    [SerializeField] private GameObject blackSquare;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFading && fadeTimer < 1f) {
            fadeTimer += Time.unscaledDeltaTime;
            blackSquare.GetComponent<SpriteRenderer>().color = new Color(0,0,0,Mathf.Lerp(1,0,fadeTimer));
        }
        else if (isFading && fadeTimer >= 1f) {
            isFading = false;
        }
    }
}
