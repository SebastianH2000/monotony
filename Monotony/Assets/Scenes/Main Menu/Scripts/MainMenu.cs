using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace SebastiansNamespace 
{
    public class MainMenu : MonoBehaviour
    {
        public bool gameStarted = false;
        public GameObject[] textArray = new GameObject[10];
        private float[] textSelections = new float[10];
        private float timer;
        private string lastTask = "Menu";
        private string currentTask = "Menu";
        private string nextTask;
        private bool menuOpen = true;
        private string[] taskArray = new string[4];
        private bool[] completeArray = new bool[4];

        private float changingTimer = 0f;
        
        // Start is called before the first frame update
        void Start()
        {
            SavePlayerData.sanity = 15f;

            Debug.Log("Start");
            changingTimer = 0;
            textSelections[Random.Range(0,textSelections.Length)] = 1f;
            taskArray = new string[4] {"Getting Ready","School","Grocery","Web"};
            for (int i = 0; i < completeArray.Length; i++) {
                completeArray[i] = false;
            }
            menuOpen = true;
            if (nextTask == "") {
                //nextTask = "Web";
            }
            nextTask = "Web";
        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log(SavePlayerData.sanity);

            if (menuOpen) {
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
                    changingTimer += Time.deltaTime/4;
                    textSelections[stringToArray(nextTask)] = Mathf.Lerp(0,1,changingTimer);
                    if (lastTask != "Menu") {
                        textSelections[stringToArray(lastTask)] = Mathf.Lerp(1,0,changingTimer);
                    }
                }
                else {
                    textSelections[stringToArray(nextTask)] = 1;
                }


                float screenYPos = 2.05f;
                for (int i = 0; i < textArray.Length; i++) {
                    float textColorR = Mathf.Lerp(0.38f,0.63f,textSelections[i]);
                    float textColorG = Mathf.Lerp(0.41f,0.63f,textSelections[i]);
                    float textColorB = Mathf.Lerp(0.42f,0.63f,textSelections[i]);
                    textArray[i].GetComponent<TextMeshProUGUI>().color = new Color(textColorR,textColorG,textColorB,1);
                    textArray[i].transform.localScale = new Vector3(1.4f+textSelections[i],1.4f+textSelections[i],1);
                    textArray[i].transform.position = new Vector3(0,screenYPos,0);
                    screenYPos += -0.8f*(textSelections[i]/3f+0.8f);
                }
            }
            if (menuOpen) {
                this.GetComponent<Collider2D>().enabled = true;
            }
            else {
                this.GetComponent<Collider2D>().enabled = false;
            }
        }

        void OnMouseDown() {
            //Debug.Log(menuOpen);
            Debug.Log(nextTask);
            if (menuOpen) {
                if (nextTask == "Web") {
                    SceneManager.LoadScene("WebScene");
                    lastTask = currentTask;
                    currentTask = nextTask;
                    nextTask = "Class";
                    menuOpen = false;
                }
                else {
                    SceneManager.LoadScene("ClassScene");
                    lastTask = currentTask;
                    currentTask = nextTask;
                    nextTask = "Getting Ready";
                    menuOpen = false;
                }
            }
        }

        int stringToArray(string inputString) {
            if (inputString == "Getting Ready") {
                return 0;
            }
            else if (inputString == "School") {
                return 4;
            }
            else if (inputString == "Grocery") {
                return 6;
            }
            else {
                return 8;
            }
        }
    }
}