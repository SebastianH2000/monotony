using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using SebastiansNamespace;

public class video_control : MonoBehaviour
{
    VideoPlayer vidPlayer;
    private bool isPlaying = false;

    void Awake()
    {
        vidPlayer = GetComponent<VideoPlayer>();
        vidPlayer.Play();
        vidPlayer.Pause();
        GameObject.Find("FadeOut").GetComponent<FadeOut>().changeScene = false;
    }   

    void Update() {
        if (!GameObject.Find("FadeIn").GetComponent<FadeIn>().isFading && !isPlaying) {
            isPlaying = true;
            vidPlayer.Play();
            vidPlayer.loopPointReached += changeScene;
        }
    }

    void changeScene(VideoPlayer vp)
    {
        /*SceneManager.LoadScene("WebScene");
        SavePlayerData.lastTask = SavePlayerData.currentTask;
        SavePlayerData.currentTask = SavePlayerData.nextTask;
        SavePlayerData.nextTask = "Class";
        SavePlayerData.menuOpen = false;*/
        GameObject.Find("FadeOut").GetComponent<FadeOut>().isFading = true;
        StartCoroutine(fadeTimer());
        //SceneManager.LoadScene("WebScene");//the scene that you want to load after the video has ended.
    }
   
    IEnumerator fadeTimer() {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GettingReadyScene");
    }
}
