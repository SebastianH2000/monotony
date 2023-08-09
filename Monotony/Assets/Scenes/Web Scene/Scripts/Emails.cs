using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace SebastiansNamespace {
    public class Emails : MonoBehaviour
    {
        public GameObject emailPrefab;
        public GameObject popupPrefab;
        public GameObject[] popupPrefabEvil;

        public List<Email> emailList = new List<Email>();

        private List<string> workSubjects = new List<string>();
        private List<string> schoolSubjects = new List<string>();
        private List<string> workReplySubjects = new List<string>();
        private List<string> schoolReplySubjects = new List<string>();
        private List<string> junkSubjects = new List<string>();
        private List<string> emailTimes = new List<string>();

        private float textPromptTimer = 0;
        private bool ignoreInput = false;
        private string input = "";
        public int currentEmailClicked  = -1;
        public bool canSend = false;
        public bool browserOpen = false;
        private bool mouseHasClicked = false;

        //audio
        public AudioSource mouseClickAudio;
        public AudioSource emailRecievedAudio;
        public AudioSource closePopupAudio;
        public AudioSource sendEmailAudio;

        public AudioSource[] keyboardHitAudio;

        private int emailMax = 2;

        private float safePopupTimer = 0;
        private float safePopupTarget = 0;
        private float monsterPopupTimer = 0;
        private float monsterPopupTarget = 0;

        // Start is called before the first frame update
        void Start()
        {
            //work email subjects
            workSubjects.Add("Can you come to work tomorrow?");
            workSubjects.Add("Take over my shift tomorrow");
            workSubjects.Add("The bean lady is back");
            workSubjects.Add("Can you lock up tomorrow night?");
            workSubjects.Add("You need to work extra hours tomorrow");

            workReplySubjects.Add("I'll be there");
            workReplySubjects.Add("Sure");
            workReplySubjects.Add("Oh dear god, why");
            workReplySubjects.Add("Ok");
            workReplySubjects.Add("Sounds good");

            schoolSubjects.Add("Is this your computer?");
            schoolSubjects.Add("When can I return your notebook?");
            schoolSubjects.Add("Found your notebook on the 6th floor.");
            schoolSubjects.Add("Hang out tomorrow?");
            schoolSubjects.Add("Here are our morning lecture notes.");
            schoolSubjects.Add("You all have homework tonight!");

            schoolReplySubjects.Add("Nope, that's not mine");
            schoolReplySubjects.Add("Tomorrow's good");
            schoolReplySubjects.Add("Thank you so much");
            schoolReplySubjects.Add("Sorry, not free");
            schoolReplySubjects.Add("ty dude");
            schoolReplySubjects.Add("Thanks for the reminder");

            junkSubjects.Add("Correct all grammar errors");
            junkSubjects.Add("10 hot horrors in your area");
            junkSubjects.Add("Click to download 100+ free ebooks!");
            junkSubjects.Add("Save up to 20% in prints this weekend!");
            junkSubjects.Add("STATUS: Your package is ready to ship");
            junkSubjects.Add("Click here to claim your raffle awards!");
            junkSubjects.Add("Plant of the month: Corpse Flower");
            junkSubjects.Add("Your phone got hacked!");
            junkSubjects.Add("Click to track your shipment.");
            junkSubjects.Add("16 $0 Meals $0 Shipping On 1st Box");

            emailTimes.Add("4:09PM");
            emailTimes.Add("5:36PM");
            emailTimes.Add("2:49PM");
            emailTimes.Add("12:00PM");
            emailTimes.Add("6:09PM");
            emailTimes.Add("1:34PM");
            emailTimes.Add("6:09AM");
            emailTimes.Add("1:34AM");
            emailTimes.Add("6:09AM");
            emailTimes.Add("1:34AM");
            emailTimes.Add("3:09AM");
            emailTimes.Add("4:34AM");
            emailTimes.Add("3 FREAKING AM");

            safePopupTimer = 0;
            safePopupTarget = Random.Range(3f,8f);
            monsterPopupTimer = 0;
            monsterPopupTarget = Random.Range(3f,8f);

            StartEmails();
        }

        // Update is called once per frame
        void Update()
        {
            if (!mouseHasClicked) {
                if (Input.GetMouseButton(0)) {
                    mouseHasClicked = true;
                    mouseClickAudio.Play();
                }
            }
            else {
                if (!Input.GetMouseButton(0)) {
                    mouseHasClicked = false;
                }
            }
            if (browserOpen) {
                this.transform.parent.gameObject.GetComponent<Alpha>().setAlpha(1f);
                float replyFieldAlpha = this.transform.Find("Reply Window").transform.Find("Reply Window Canvas").transform.Find("Reply Field").gameObject.GetComponent<TextMeshProUGUI>().color.a;
                    
                if (currentEmailClicked >= 0 && emailList[currentEmailClicked].author != "Junk" && input.Length == 0) {
                    textPromptTimer += Time.deltaTime;
                    float lightness = (Mathf.Sin(textPromptTimer*8)+2)/3;
                    this.transform.Find("Reply Window").transform.Find("Reply Window Canvas").transform.Find("Reply Field").gameObject.GetComponent<TextMeshProUGUI>().color = Color.HSVToRGB(1, 0, lightness);
                    this.transform.Find("Reply Window").transform.Find("Reply Window Canvas").transform.Find("Reply Field").gameObject.GetComponent<Alpha>().setAlpha(replyFieldAlpha);
                }
                else if (currentEmailClicked >= 0 && input.Length > 0) {
                    textPromptTimer += Time.deltaTime;
                    this.transform.Find("Reply Window").transform.Find("Reply Window Canvas").transform.Find("Reply Field").gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1,1,1,1);
                    this.transform.Find("Reply Window").transform.Find("Reply Window Canvas").transform.Find("Reply Field").gameObject.GetComponent<Alpha>().setAlpha(replyFieldAlpha);
                }
                else if (currentEmailClicked >= 0) {
                    this.transform.Find("Reply Window").transform.Find("Reply Window Canvas").transform.Find("Reply Field").gameObject.GetComponent<TextMeshProUGUI>().color = new Color(0.6f,0,0,1);
                }
                else if (currentEmailClicked < 0) {
                    textPromptTimer += Time.deltaTime;
                    float lightness = (Mathf.Sin(textPromptTimer*8)+2)/3;
                    this.transform.Find("Reply Window").transform.Find("Reply Window Canvas").transform.Find("Reply Field").gameObject.GetComponent<TextMeshProUGUI>().color = Color.HSVToRGB(1, 0, lightness);
                    this.transform.Find("Reply Window").transform.Find("Reply Window Canvas").transform.Find("Reply Field").gameObject.GetComponent<Alpha>().setAlpha(replyFieldAlpha);
                }

                for (int i = 0; i < emailList.Count; i++) {
                    if (emailList[i].isShifting) {
                        if (emailList[i].shiftTimer >= 1) {
                            emailList[i].isShifting = false;
                            emailList[i].emailObject.transform.localPosition = emailList[i].shiftPos2;
                        }
                        else {
                            emailList[i].shiftTimer += Time.deltaTime;
                            emailList[i].emailObject.transform.localPosition = Vector3.Lerp(emailList[i].shiftPos1,emailList[i].shiftPos2,emailList[i].shiftTimer);
                        }
                    }
                }

                string targetString = this.transform.Find("Reply Window").transform.Find("Reply Window Canvas").transform.Find("Reply Field").gameObject.GetComponent<TextMeshProUGUI>().text;
                if (Input.inputString.Length > 0 && !ignoreInput && currentEmailClicked > -1)
                {
                    if (Input.GetKeyDown(KeyCode.Return) && canSend) 
                    {
                        emailSent();
                    }
                    else {
                        char key = Input.inputString[0];
                        if (char.ToLower(key) == char.ToLower(targetString[input.Length])) {
                            keyboardHitAudio[Random.Range(0,(keyboardHitAudio.Length))].Play();
                            input += targetString[input.Length];
                        }

                        if (input == targetString && canSend == false)
                        {
                            GameObject.Find("Send Bubble").GetComponent<SpriteRenderer>().color = new Color(0.85f,0.85f,0.85f,1);
                            canSend = true;
                        }
                        else if (input != targetString) {
                            canSend = false;
                        }

                        //display input
                        //computer.UpdateDisplayText(displayText);
                        this.transform.Find("Reply Window").transform.Find("Reply Window Canvas").transform.Find("Reply Input").gameObject.GetComponent<TextMeshProUGUI>().text = input;
                    }
                }

                if (Input.GetKeyDown(KeyCode.Return) && canSend) 
                {
                    emailSent();
                }

                //random emails
                if (Random.Range(0f,1f) > 0.9996f && emailMax > 0) 
                {
                    emailMax --;
                    emailRecievedAudio.Play();
                    addEmail();
                }

                //random pop-ups
                /*if (Random.Range(0f,1f) > 0.9997f) 
                {
                    if (Random.Range(0f,1f) > 0.5f) {
                        Instantiate(popupPrefab, this.transform.parent.transform);
                    }
                    else {
                        Instantiate(popupPrefabEvil[Random.Range(0,popupPrefabEvil.Length)], this.transform.parent.transform);
                    }
                }*/

                if (monsterPopupTimer > monsterPopupTarget) {
                    Instantiate(popupPrefabEvil[Random.Range(0,popupPrefabEvil.Length)], this.transform.parent.transform);
                    monsterPopupTimer = 0;
                    monsterPopupTarget = Random.Range(6f,10f);
                }
                else {
                    monsterPopupTimer += Time.deltaTime;
                }
                if (safePopupTimer > safePopupTarget) {
                    Instantiate(popupPrefab, this.transform.parent.transform);
                    safePopupTimer = 0;
                    safePopupTarget = Random.Range(6f,10f);
                }
                else {
                    safePopupTimer += Time.deltaTime;
                }
            }
            else {
                this.transform.parent.gameObject.GetComponent<Alpha>().setAlpha(0f);
            }

            GameObject.Find("Email Count Display").GetComponent<TextMeshProUGUI>().text = emailList.Count.ToString();
        }

        void StartEmails() {
            emailList = new List<Email>(0);

            addEmail();
            addEmail();
            addEmail();
            addEmail();
            addEmail();
            addEmail();
            addEmail();
            addEmail();
            //addEmail();
            //addEmail();

            //removeEmail(0);
        }

        void addEmail() {
            Email currentEmail = new Email("","","",true, emailList.Count);
            currentEmail.emailObject = Instantiate(emailPrefab, this.transform);
            currentEmail.emailObject.name = emailList.Count.ToString();

            if (emailList.Count > 0 && emailList[emailList.Count - 1].isShifting) {
                currentEmail.isShifting = true;
                currentEmail.shiftTimer = emailList[emailList.Count - 1].shiftTimer;
                currentEmail.shiftPos1 = new Vector3(0,(0.44f - (0.09f * (emailList.Count + 1))),-0.1f);
                currentEmail.shiftPos2 = new Vector3(0,(0.44f - (0.09f * emailList.Count)),-0.1f);
            }
            else {
                currentEmail.emailObject.transform.localPosition = new Vector3(0,(0.44f - (0.09f * emailList.Count)),-0.1f);
            }
            //currentEmail.emailObject.transform.Find("Canvas").transform.Find("Subject").gameObject.GetComponent<TextMeshProUGUI>().text = emailList.Count.ToString();
            if (Random.Range(0f,1f) > 0.66f) {
                //work
                currentEmail.isUrgent = true;
                currentEmail.author = "Work";
                currentEmail.emailObject.transform.Find("Canvas").transform.Find("Author").gameObject.GetComponent<TextMeshProUGUI>().text = "Work";

                currentEmail.replyIndex = Random.Range(0,(int)(workSubjects.Count));
                currentEmail.emailObject.transform.Find("Canvas").transform.Find("Subject").gameObject.GetComponent<TextMeshProUGUI>().text = workSubjects[currentEmail.replyIndex];
                currentEmail.subject = workSubjects[currentEmail.replyIndex];
                currentEmail.replyString = workReplySubjects[currentEmail.replyIndex];
            }
            else if (Random.Range(0f,1f) > 0.44f) {
                //school
                currentEmail.isUrgent = true;
                currentEmail.author = "School";
                currentEmail.emailObject.transform.Find("Canvas").transform.Find("Author").gameObject.GetComponent<TextMeshProUGUI>().text = "School";

                currentEmail.replyIndex = Random.Range(0,(int)(schoolSubjects.Count));
                currentEmail.emailObject.transform.Find("Canvas").transform.Find("Subject").gameObject.GetComponent<TextMeshProUGUI>().text = schoolSubjects[currentEmail.replyIndex];
                currentEmail.subject = schoolSubjects[currentEmail.replyIndex];
                currentEmail.replyString = schoolReplySubjects[currentEmail.replyIndex];
            }
            else {
                //junk
                currentEmail.isUrgent = false;
                currentEmail.author = "Junk";
                currentEmail.emailObject.transform.Find("Canvas").transform.Find("Author").gameObject.GetComponent<TextMeshProUGUI>().text = "Junk";
                currentEmail.author = "Junk";
                currentEmail.emailObject.transform.Find("Canvas").transform.Find("Subject").gameObject.GetComponent<TextMeshProUGUI>().text = junkSubjects[Random.Range(0,(int)(junkSubjects.Count))];
                currentEmail.subject = junkSubjects[currentEmail.replyIndex];
                currentEmail.replyString = "";
            }

            if (currentEmail.isUrgent == false) {
                GameObject.Destroy(currentEmail.emailObject.transform.Find("Canvas").transform.Find("Urgency").gameObject);
                GameObject.Destroy(currentEmail.emailObject.transform.Find("Sprites").transform.Find("Highlight Bubble").gameObject);
            }
            currentEmail.emailObject.transform.Find("Canvas").transform.Find("Time").gameObject.GetComponent<TextMeshProUGUI>().text = emailTimes[Random.Range(0,(int)(emailTimes.Count))];
            currentEmail.ID = emailList.Count;
            currentEmail.emailObject.transform.Find("Sprites").transform.Find("Background").gameObject.tag = "EmailBackground";

            if (GameObject.Find("EmailTab").GetComponent<SpriteRenderer>().color.r < 0.7 || !browserOpen) {
                currentEmail.emailObject.GetComponent<Alpha>().setAlpha(0f);
            }

            emailList.Add(currentEmail);
        }

        void removeEmail(int indexInList) {
            emailList[indexInList].Delete();
            emailList.RemoveAt(indexInList);
            currentEmailClicked = -1;
            for (int i = indexInList; i < emailList.Count; i++) {
                emailList[i].ID = i;
                emailList[i].emailObject.name = i.ToString();

                emailList[i].isShifting = true;
                emailList[i].shiftTimer = 0;
                emailList[i].shiftPos1 = emailList[i].emailObject.transform.localPosition;
                emailList[i].shiftPos2 = new Vector3(0,(0.44f - (0.09f * i)),-0.1f);
            }
            sendEmailAudio.Play();
            if (emailMax == 0 && emailList.Count == 0) {
                GameObject.Find("FadeOut").GetComponent<FadeOut>().isFading = true;
            }
            else if (emailMax > 0 && emailList.Count == 0) {
                emailMax --;
                emailRecievedAudio.Play();
                addEmail();
            }
        }

        public void emailClicked(int emailID) {
            if (emailID != currentEmailClicked) {
                this.transform.Find("Reply Window").transform.Find("Reply Window Canvas").transform.Find("Reply Field").gameObject.GetComponent<TextMeshProUGUI>().text = emailList[emailID].replyString;
                this.transform.Find("Reply Window").transform.Find("Reply Window Canvas").transform.Find("Reply Input").gameObject.GetComponent<TextMeshProUGUI>().text = "";
                this.transform.Find("Reply Window").transform.Find("Reply Window Canvas").transform.Find("Reply Subject").gameObject.GetComponent<TextMeshProUGUI>().text = ("Re: " + emailList[emailID].subject);
                this.transform.Find("Reply Window").transform.Find("Reply Window Canvas").transform.Find("Reply Recipient").gameObject.GetComponent<TextMeshProUGUI>().text = ("To: " + emailList[emailID].author);
                input = "";
                currentEmailClicked = emailID;
                if (emailList[emailID].author == "Junk") {
                    GameObject.Find("Send Text").GetComponent<TextMeshProUGUI>().text = "Delete";
                    GameObject.Find("Send Bubble").GetComponent<SpriteRenderer>().color = new Color(0.85f,0.85f,0.85f,1);
                    canSend = true;
                    ignoreInput = true;
                }
                else {
                    GameObject.Find("Send Text").GetComponent<TextMeshProUGUI>().text = "Send";
                    GameObject.Find("Send Bubble").GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1);
                    canSend = false;
                    ignoreInput = false;
                }
            }
        }

        public void emailSent() {
            if (canSend && currentEmailClicked >= 0) {
                this.transform.Find("Reply Window").transform.Find("Reply Window Canvas").transform.Find("Reply Field").gameObject.GetComponent<TextMeshProUGUI>().text = "";
                this.transform.Find("Reply Window").transform.Find("Reply Window Canvas").transform.Find("Reply Input").gameObject.GetComponent<TextMeshProUGUI>().text = "";
                this.transform.Find("Reply Window").transform.Find("Reply Window Canvas").transform.Find("Reply Subject").gameObject.GetComponent<TextMeshProUGUI>().text = "Re:";
                this.transform.Find("Reply Window").transform.Find("Reply Window Canvas").transform.Find("Reply Recipient").gameObject.GetComponent<TextMeshProUGUI>().text = "To:";
                GameObject.Find("Send Bubble").GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1);
                input = "";
                removeEmail(currentEmailClicked);
            }
        }
    }

    public class Email 
    {
        public string subject;
        public string author;
        public string time;
        public bool isUrgent;
        public int ID;
        public GameObject emailObject;
        public bool isShifting = false;
        public Vector3 shiftPos1;
        public Vector3 shiftPos2;
        public float shiftTimer = 0;

        public int replyIndex;
        public string replyString;

        //constructor function
        public Email(string emailSubject, string emailAuthor, string emailTime, bool emailIsUrgent, int ID) {
            subject = emailSubject;
            author = emailAuthor;
            time = emailTime;
            isUrgent = emailIsUrgent;
        }

        public void Delete() {
            GameObject.Destroy(emailObject);
        }
    }
}