using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SebastiansNamespace;

public class TryAgain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown() {
        SavePlayerData.sanity += 0.5f;
        SavePlayerData.sanity = Mathf.Clamp(SavePlayerData.sanity, 0, 1);
        SavePlayerData.lookingAtMonster = false;
        SavePlayerData.monsterDistance = 0;
        SavePlayerData.day = 1;
        SavePlayerData.time = 0;
        SavePlayerData.taskNumber = 0;

        SavePlayerData.currentTask = "Menu";
        SavePlayerData.nextTask = "GettingReady";
        SavePlayerData.menuOpen = true;
        SavePlayerData.isDead = false;

        SavePlayerData.taskArray = new string[4];
        SavePlayerData.completedArray = new bool[4];
        SavePlayerData.hasWatchedIntro = false;
        GameObject.Find("FadeOut").GetComponent<FadeOut>().isFading = true;
    }
}
