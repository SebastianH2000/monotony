using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace SebastiansNamespace 
{
    public class MainMenu : MonoBehaviour
    {
        public GameObject[] textArray = new GameObject[10];
        private float[] textSelections = new float[10];
        private float timer;
        private bool menuOpen = true;
        private string[] taskArray = new string[4];
        private bool[] completedArray = new bool[4];

        private float changingTimer = 0f;
        
        // Start is called before the first frame update
        void Start()
        {
            changingTimer = 0;
            textSelections[Random.Range(0,textSelections.Length)] = 1f;
            //taskArray = new string[4] {"GettingReady","Class","Grocery","Web"};
            taskArray = new string[6] {"GettingReady","Class","Grocery","Bussing","Web","NextDay"};
            for (int i = 0; i < SavePlayerData.completedArray.Length; i++) {
                SavePlayerData.completedArray[i] = false;
            }
            SavePlayerData.menuOpen = true;
        }

        // Update is called once per frame
        void Update()
        {
            /*timer += Time.deltaTime*3;
            int timerClamp = (int)timer % 10;
            int nextClamp = (int)(timer + 1) % 10;*/
            for (int i = 0; i < textArray.Length; i++) {
                textSelections[i] = 0f;
            }
            /*textSelections[timerClamp] = Mathf.Lerp(1,0,((timer % 10)-timerClamp));
            textSelections[nextClamp] = Mathf.Lerp(0,1,((timer % 10)-timerClamp));*/
            //textSelections[8] = 1f;
            if (changingTimer < 1) {
                changingTimer += Time.deltaTime;
                textSelections[stringToArray(SavePlayerData.nextTask)] = Mathf.Lerp(0,1,changingTimer);
                if (SavePlayerData.currentTask != "Menu") {
                    textSelections[stringToArray(SavePlayerData.currentTask)] = Mathf.Lerp(1,0,changingTimer);
                }
            }
            else {
                Debug.Log(SavePlayerData.nextTask);
                textSelections[stringToArray(SavePlayerData.nextTask)] = 1;
            }


            float screenYPos = 2.05f;
            for (int i = 0; i < textArray.Length; i++) {
                float textColorR = Mathf.Lerp(0.38f,0.63f,textSelections[i]);
                float textColorG = Mathf.Lerp(0.41f,0.63f,textSelections[i]);
                float textColorB = Mathf.Lerp(0.42f,0.63f,textSelections[i]);
                textArray[i].GetComponent<TextMeshProUGUI>().color = new Color(textColorR,textColorG,textColorB,1);
                textArray[i].transform.localScale = new Vector3(1.4f+textSelections[i],1.4f+textSelections[i],1);
                textArray[i].transform.position = new Vector3(0,screenYPos-textSelections[i]/8f,0);
                screenYPos += -0.8f*(textSelections[i]/3f+0.8f);
            }
        }

        void OnMouseDown() {
            GameObject.Find("FadeOut").GetComponent<FadeOut>().isFading = true;
        }
        public void nextScene() {
            if (!SavePlayerData.hasWatchedIntro) {
                /*SavePlayerData.taskNumber++;
                SavePlayerData.currentTask = "Menu";
                SavePlayerData.nextTask = "Class";
                SavePlayerData.menuOpen = false;
                SavePlayerData.hasWatchedIntro = true;
                SceneManager.LoadScene("IntroVideo");*/
                SavePlayerData.taskNumber = 5;
                SavePlayerData.currentTask = "Menu";
                SavePlayerData.nextTask = "Web";
                SavePlayerData.menuOpen = false;
                SavePlayerData.hasWatchedIntro = true;
                SceneManager.LoadScene("WebScene");
            }
            else if (SavePlayerData.taskNumber < 5){
                Debug.Log(SavePlayerData.nextTask);
                SavePlayerData.currentTask = SavePlayerData.nextTask;
                SavePlayerData.menuOpen = false;
                SavePlayerData.taskNumber++;
                SavePlayerData.nextTask = taskArray[SavePlayerData.taskNumber];
                Debug.Log(SavePlayerData.currentTask + "Scene");
                SceneManager.LoadScene(SavePlayerData.currentTask + "Scene");
                changingTimer = 0;
            }
            else {

            }
            /*else if (menuOpen) {
                if (SavePlayerData.nextTask == "Web") {
                    SceneManager.LoadScene("WebScene");
                    SavePlayerData.lastTask = SavePlayerData.currentTask;
                    SavePlayerData.currentTask = SavePlayerData.nextTask;
                    SavePlayerData.nextTask = "Class";
                    SavePlayerData.menuOpen = false;
                }
                else {
                    SceneManager.LoadScene("ClassScene");
                    SavePlayerData.lastTask = SavePlayerData.currentTask;
                    SavePlayerData.currentTask = SavePlayerData.nextTask;
                    SavePlayerData.nextTask = "Getting Ready";
                    SavePlayerData.menuOpen = false;
                }
            }*/
        }

        int stringToArray(string inputString) {
            if (inputString == "GettingReady") {
                return 0;
            }
            else if (inputString == "Class") {
                return 4;
            }
            else if (inputString == "Grocery") {
                return 6;
            }
            else if (inputString == "Bussing") {
                return 5;
            }
            else if (inputString == "NextDay") {
                return 9;
            }
            else {
                return 8;
            }
        }
    }
}