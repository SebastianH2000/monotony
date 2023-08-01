using System.Collections;
using UnityEngine;
using TMPro;

namespace RyansNamespace
{
    [RequireComponent(typeof(Animator))]
    public class Bean : Drag
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI displayText;

        [Header("Barcode")]
        [SerializeField] private string barcode;
        [SerializeField] private string inputPrompt;
        private bool isBarcodeShown = false;
        private bool ignoreInput = false;
        private string input;

        private bool isScanning;

        private State currentState = State.BARCODE;
        private Animator AN;

        private enum State
        {
            BARCODE,
            DRAG
        }

        #region Default

        public override void Awake() {
            base.Awake();
            AN = GetComponent<Animator>();
        }

        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
            displayText.text = inputPrompt;
        }

        // Update is called once per frame
        public override void Update()
        {
            base.Update();

            if (currentState == State.BARCODE)
                GetAndDisplayInput();
        }

        #endregion

        #region Barcode

        private void GetAndDisplayInput()
        {
            // get input

            if (Input.inputString.Length > 0 && !ignoreInput)
            {
                char key = Input.inputString[0];

                if (char.IsDigit(key))
                {
                    input += key;

                    if (input.Length == barcode.Length)
                    {
                        if (input == barcode)
                        {
                            StartCoroutine(SuccessSequence());
                            ignoreInput = true;
                        }
                        else
                        {
                            StartCoroutine(RetrySequence());
                            ignoreInput = true;
                        }
                    }
                }
                else if (key == '\b' && input.Length >= 1)
                {
                    input = input.Remove(input.Length - 1);
                }

                // display input

                displayText.text = "";

                for (int i = 0; i < input.Length; i++)
                {
                    if (i < input.Length - 1)
                        displayText.text += input[i] + " ";
                    else
                        displayText.text += input[i];
                }
            }
        }

        private IEnumerator RetrySequence()
        {
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(FlashingText("INCORRECT!", 3, 0.5f));
            yield return StartCoroutine(FlashingText("TRY AGAIN!", 3, 0.5f));

            input = "";
            displayText.text = inputPrompt;
            ignoreInput = false;
        }

        private IEnumerator SuccessSequence()
        {
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(FlashingText("CORRECT!", 3, 0.5f));

            if (isBarcodeShown)
            {
                AN.Play("HideBarCode");
                isBarcodeShown = false;
            }

            displayText.text = "DRAG ACROSS";
            currentState = State.DRAG;
        }

        #endregion

        public override void OnMouseDown()
        {
            switch (currentState)
            {
                case State.BARCODE:
                    if (!isBarcodeShown)
                    {
                        AN.Play("ShowBarCode");
                        isBarcodeShown = true;
                    }
                    else
                    {
                        AN.Play("HideBarCode");
                        isBarcodeShown = false;
                    }
                    break;
                case State.DRAG:
                    base.OnMouseDown();
                    break;
            }
        }

        public override void OnMouseUp()
        {
            if (currentState == State.DRAG)
                base.OnMouseUp();
        }

        private IEnumerator FlashingText(string text, int times, float delay) {
            for (int i = 0; i < times; i++) {
                displayText.text = text;
                yield return new WaitForSeconds(delay);
                displayText.text = "";
                yield return new WaitForSeconds(delay);
            }
        }

        #region Scanner

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Scanner"))
            {
                float otherRelativeYPos = other.transform.position.y - other.bounds.size.y / 2f;
                float relativeYPos = transform.position.y - boxCollider.bounds.size.y / 2f;

                if (Mathf.Abs(otherRelativeYPos - relativeYPos) < 0.1f && transform.position.x > other.transform.position.x)
                    isScanning = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Scanner"))
            {
                if (isScanning)
                {
                    float otherRelativeYPos = other.transform.position.y - other.bounds.size.y / 2f;
                    float relativeYPos = transform.position.y - boxCollider.bounds.size.y / 2f;

                    if (Mathf.Abs(otherRelativeYPos - relativeYPos) < 0.1f && transform.position.x < other.transform.position.x)
                        StartCoroutine(FlashingText("SCANNED!", 3, 0.5f));
                    else
                        StartCoroutine(FlashingText("SCAN FAILED", 3, 0.5f));

                    isScanning = false;
                }
            }
        }

        #endregion
    }
}