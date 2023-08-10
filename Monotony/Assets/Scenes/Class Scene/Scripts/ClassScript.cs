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

        private float slideStartTimer = 0;

        public bool handClicked = true;

        public GameObject check1;
        public GameObject check2;
        public GameObject check3;

        private string input = "";
        private bool canSend = true;

        private bool waitingForTeacher = false;

        public AudioSource[] keyboardHitAudio;
        public AudioSource[] questionAudio;
        public AudioSource correctAudio;

        public GameObject teacherStandingMonster;
        public GameObject teacherQuestionMonster;
        private bool isMonster = true;
        private float monsterTimer = 0;
        private float monsterTarget = 0;

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

            teacherStandingMonster.GetComponent<ClassMonster>().hide();
            teacherQuestionMonster.GetComponent<ClassMonster>().hide();
        }

        // Update is called once per frame
        void Update()
        {
            if (monsterTimer > monsterTarget) {
                monsterTimer = 0;
                isMonster = !isMonster;
                if (isMonster) {
                    monsterTarget = Random.Range(3f,5f);
                    if (waitingForTeacher) {
                        teacherQuestionMonster.GetComponent<ClassMonster>().show();
                    }
                    else {
                        teacherStandingMonster.GetComponent<ClassMonster>().show();
                    }
                    teacherStanding.GetComponent<SpriteRenderer>().enabled = false;
                    teacherAsking.GetComponent<SpriteRenderer>().enabled = false;
                }
                else {
                    monsterTarget = Random.Range(10f,20f);
                    teacherStandingMonster.GetComponent<ClassMonster>().hide();
                    teacherQuestionMonster.GetComponent<ClassMonster>().hide();
                    if (handClicked) {
                        teacherStanding.GetComponent<SpriteRenderer>().enabled = true;
                    }
                    else {
                        teacherAsking.GetComponent<SpriteRenderer>().enabled = true;
                    }
                }
            }
            else {
                monsterTimer += Time.deltaTime;
            }

            if (waitingForTeacher) {
                teacherQuestionTimer += Time.deltaTime;
                if (teacherQuestionTimer > 0.5f && !isMonster) {
                    teacherStanding.GetComponent<SpriteRenderer>().enabled = false;
                    teacherAsking.GetComponent<SpriteRenderer>().enabled = true;
                    teacherQuestionTimer = -100;
                    questionAudio[Random.Range(0,(questionAudio.Length))].Play();

                    handClicked = false;
                }
            }

            //slides
            if (slideStartTimer > 1.5f) {
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

                if ((char.IsLetter(key) || (key == " "[0] && input.Length > 0)) && input.Length < targetString.Length)
                {
                    input += key;
                    keyboardHitAudio[Random.Range(0,(keyboardHitAudio.Length))].Play();

                    if (input.Length == targetString.Length)
                    {
                        if (input.ToLower() == targetString.ToLower()) {
                            correctAudio.Play();
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
                                if (slideShownList.Count > 3) {
                                    waitingForTeacher = true;
                                }
                                else {
                                    GameObject.Find("FadeOut").GetComponent<FadeOut>().isFading = true;
                                }
                            }
                        }
                    } else if (key == '\b' && input.Length >= 1)
                    {
                        keyboardHitAudio[Random.Range(0,(keyboardHitAudio.Length))].Play();
                        input = input.Remove(input.Length - 1);
                    }
                } else if (key == '\b' && input.Length >= 1)
                {
                    input = input.Remove(input.Length - 1);
                }
                computerText.GetComponent<TextMeshProUGUI>().text = input;
            }            
        }

        public void clickHand() {
            if (handClicked == false && !isMonster) {
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
