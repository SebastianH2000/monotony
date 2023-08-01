using System.Collections;
using UnityEngine;

namespace RyansNamespace
{
    [RequireComponent(typeof(Animator))]
    public class Bean : Drag
    {
        [Header("References")]
        [SerializeField] private Computer computer;

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

        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();

            AN = GetComponent<Animator>();
            computer.UpdateDisplayText(inputPrompt);
        }

        // Update is called once per frame
        public override void Update()
        {
            base.Update();

            if (currentState == State.BARCODE)
            {
                GetAndDisplayInput();
            }
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

                string displayText = "";

                for (int i = 0; i < input.Length; i++)
                {
                    if (i < input.Length - 1)
                        displayText += input[i] + " ";
                    else
                        displayText += input[i];
                }

                computer.UpdateDisplayText(displayText);
            }
        }

        private IEnumerator RetrySequence()
        {
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(computer.FlashingText("INCORRECT!", 3, 0.5f));
            yield return StartCoroutine(computer.FlashingText("TRY AGAIN!", 3, 0.5f));

            input = "";
            computer.UpdateDisplayText(inputPrompt);
            ignoreInput = false;
        }

        private IEnumerator SuccessSequence()
        {
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(computer.FlashingText("CORRECT!", 3, 0.5f));

            if (isBarcodeShown)
            {
                AN.Play("HideBarCode");
                isBarcodeShown = false;
            }

            computer.UpdateDisplayText("DRAG ACROSS");
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
                        StartCoroutine(computer.FlashingText("SCANNED!", 3, 0.5f));
                    else
                        StartCoroutine(computer.FlashingText("SCAN FAILED", 3, 0.5f));

                    isScanning = false;
                }
            }
        }

        #endregion
    }
}