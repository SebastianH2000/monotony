using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SebastiansNamespace;

public class FadeOut : MonoBehaviour
{
    public bool isFading = false;
    private float fadeTimer = 0f;
    [SerializeField] private GameObject blackSquare;
    public bool changeScene = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFading && fadeTimer < 1f) {
            if (SceneManager.GetActiveScene().name != "MainMenuScene") {
                fadeTimer += Time.deltaTime/2.5f;
            }
            else {
                fadeTimer += Time.deltaTime;
            }
            if (!SavePlayerData.isDead) {
                blackSquare.GetComponent<SpriteRenderer>().color = new Color(0,0,0,Mathf.Lerp(0,1,fadeTimer));
            }
        }
        else if (isFading && fadeTimer >= 1f && SavePlayerData.isDead) {
            SceneManager.LoadScene("MainMenuScene");
        }
        else if (isFading && fadeTimer >= 1f && SavePlayerData.sanity <= 0) {
            SavePlayerData.isDead = true;
            SceneManager.LoadScene("OutroVideo");
        }
        else if (isFading && fadeTimer >= 1f && changeScene) {
            if (SceneManager.GetActiveScene().name != "MainMenuScene") {
                SceneManager.LoadScene("MainMenuScene");
            }
            else {
                GameObject.Find("Menu Controller").GetComponent<MainMenu>().nextScene();
            }
        }
    }
}
