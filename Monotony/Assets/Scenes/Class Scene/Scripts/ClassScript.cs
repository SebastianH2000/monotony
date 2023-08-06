using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SebastiansNamespace;

namespace SebastiansNamespace {
    public class ClassScript : MonoBehaviour
    {
        // Start is called before the first frame update

        public GameObject teacherStanding;
        public GameObject teacherAsking;

        public GameObject handLeftRaising;
        public GameObject handLeftResting;
        public GameObject handRight;

        public GameObject computerText;

        public GameObject[] slideShownArray = new GameObject[7];
        private List<GameObject> slideShownList = new List<GameObject>();
        private List<string[]> slideAnswers = new List<string[]>();
        private int slideAnswerPosition = 0;
        private int currentSlide = 0;

        private float teacherQuestionTimer = 0;
        private float teacherQuestionTarget = 0.5f;

        private float slideStartTimer = 0;

        public bool handClicked = true;

        public GameObject check1;
        public GameObject check2;
        public GameObject check3;

        private string input = "";
        private bool canSend = true;

        private bool waitingForTeacher = false;
        void Start()
        {
            for (int i = 0; i < 7; i++) {
                slideShownArray[i].GetComponent<SpriteRenderer>().enabled = false;
                slideShownList.Add(slideShownArray[i]);
            }

            //slideAnswers.Add(new int[0,0,0]);
            slideAnswers.Add(new string[3] {"mimics", "resistant", "perennial"});
            slideAnswers.Add(new string[3] {"alkaloids", "mutualistic", "resistance"});
            slideAnswers.Add(new string[2] {"sesquiterpenoids", "ecosystem"});
            slideAnswers.Add(new string[2] {"symbiotic", "mycorrhiza"});
            slideAnswers.Add(new string[3] {"fibre", "protein", "carbohydrates"});
            slideAnswers.Add(new string[3] {"ascomycota", "largest", "asci"});
            slideAnswers.Add(new string[3] {"mold", "haploid hyphae", "asexually"});

            teacherAsking.GetComponent<SpriteRenderer>().enabled = false;

            handLeftRaising.GetComponent<SpriteRenderer>().enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            //teacher questions
            /*if (teacherQuestionTimer > teacherQuestionTarget && handClicked == true) {
                teacherStanding.GetComponent<SpriteRenderer>().enabled = false;
                teacherAsking.GetComponent<SpriteRenderer>().enabled = true;

                handClicked = false;
            }
            else {
                teacherQuestionTimer += Time.deltaTime;
            }*/

            if (waitingForTeacher) {
                teacherQuestionTimer += Time.deltaTime;
                if (teacherQuestionTimer > 0.5f) {
                    teacherStanding.GetComponent<SpriteRenderer>().enabled = false;
                    teacherAsking.GetComponent<SpriteRenderer>().enabled = true;

                    handClicked = false;
                }
            }

            //slides
            if (slideStartTimer > 3) {
                slideStartTimer = -1;
                currentSlide = Random.Range(0,slideShownList.Count);
                slideShownList[currentSlide].GetComponent<SpriteRenderer>().enabled = true;
                if (slideAnswers[currentSlide].Length == 2) {
                    check3.GetComponent<SpriteRenderer>().enabled = false;
                }
                else {
                    check3.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
            else if (slideStartTimer > -1) {
                slideStartTimer += Time.deltaTime;
            }


            //input
            //string targetString = "he";
            string targetString = slideAnswers[currentSlide][slideAnswerPosition];
            if (Input.inputString.Length > 0 && canSend)
            {
                char key = Input.inputString[0];

                if ((char.IsLetter(key) || key == " "[0]) && input.Length < targetString.Length)
                {
                    Debug.Log(Input.inputString);
                    Debug.Log(targetString);
                    Debug.Log(currentSlide);
                    input += key;

                    if (input.Length == targetString.Length)
                    {
                        if (input.ToLower() == targetString.ToLower()) {
                            input = "";
                            computerText.GetComponent<TextMeshProUGUI>().text = input;
                            if (slideAnswerPosition < (slideAnswers[currentSlide].Length-1)) {
                                //checkboxes
                                check1.GetComponent<Checkbox>().On();
                                if (slideAnswerPosition == 1) {
                                    check2.GetComponent<Checkbox>().On();
                                }
                                slideAnswerPosition++;
                            }
                            else {
                                if (slideAnswerPosition == 1) {
                                    check2.GetComponent<Checkbox>().On();
                                }
                                if (check3.GetComponent<SpriteRenderer>().enabled) {
                                    check3.GetComponent<Checkbox>().On();
                                }
                                waitingForTeacher = true;
                            }
                        }
                        else {
                            computerText.GetComponent<TextMeshProUGUI>().text = input;
                            return;
                        }
                    }
                } else if (key == '\b' && input.Length >= 1)
                {
                    input = input.Remove(input.Length - 1);
                } else {
                    return;
                }
                computerText.GetComponent<TextMeshProUGUI>().text = input;
            }            
        }

        public void clickHand() {
            if (teacherQuestionTimer > 0.5f && handClicked == false) {
                handLeftResting.GetComponent<SpriteRenderer>().enabled = false;
                handLeftRaising.GetComponent<SpriteRenderer>().enabled = true;
                StartCoroutine(handTimer());
            }
        }

        IEnumerator handTimer() {
            yield return new WaitForSeconds(1f);
            teacherStanding.GetComponent<SpriteRenderer>().enabled = true;
            teacherAsking.GetComponent<SpriteRenderer>().enabled = false;

            handLeftResting.GetComponent<SpriteRenderer>().enabled = true;
            handLeftRaising.GetComponent<SpriteRenderer>().enabled = false;

            waitingForTeacher = false;
            teacherQuestionTimer = 0f;

            teacherQuestionTarget = Random.Range(3f,5f);
            teacherQuestionTimer = 0f;
            handClicked = true;
            newSlide();
        }

        void newSlide() {
            if (slideShownList.Count > 1) {
                slideShownList[currentSlide].GetComponent<SpriteRenderer>().enabled = false;
                slideShownList.RemoveAt(currentSlide);
                slideAnswers.RemoveAt(currentSlide);
                currentSlide = Random.Range(0,slideShownList.Count);
                slideShownList[currentSlide].GetComponent<SpriteRenderer>().enabled = true;
                slideAnswerPosition = 0;

                //checkboxes
                if (slideAnswers[currentSlide].Length == 2) {
                    check3.GetComponent<SpriteRenderer>().enabled = false;
                }
                else {
                    check3.GetComponent<SpriteRenderer>().enabled = true;
                }
                check1.GetComponent<Checkbox>().Off();
                check2.GetComponent<Checkbox>().Off();
                check3.GetComponent<Checkbox>().Off();
            }
        }
    }
}