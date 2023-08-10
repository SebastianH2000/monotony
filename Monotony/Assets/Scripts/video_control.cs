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
        GameObject.Find("FadeOut").GetComponent<FadeOut>().isFading = true;
        StartCoroutine(fadeTimer());
    }
   
    IEnumerator fadeTimer() {
        yield return new WaitForSeconds(1f);
        if (SavePlayerData.isDead) {
            SceneManager.LoadScene("TryAgainScene");
        }
        else {
            SceneManager.LoadScene("GettingReadyScene");
        }
    }
}
