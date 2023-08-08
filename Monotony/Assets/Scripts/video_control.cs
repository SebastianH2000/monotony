using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using SebastiansNamespace;

public class video_control : MonoBehaviour
{
    VideoPlayer vidPlayer;

    void Awake()
    {
        vidPlayer = GetComponent<VideoPlayer>();
        vidPlayer.Play();
        vidPlayer.loopPointReached += changeScene;
    }   
    void changeScene(VideoPlayer vp)
    {
        SceneManager.LoadScene("WebScene");
        SavePlayerData.lastTask = SavePlayerData.currentTask;
        SavePlayerData.currentTask = SavePlayerData.nextTask;
        SavePlayerData.nextTask = "Class";
        SavePlayerData.menuOpen = false;
        //SceneManager.LoadScene("WebScene");//the scene that you want to load after the video has ended.
    }
   
}
