using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RyansNamespace
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Bean))]
    public class Barcode : MonoBehaviour
    {
        [SerializeField] private Computer computer;

        [Header("Bar Code")]
        [SerializeField] private string barcode;
        [SerializeField] private string defaultInputText;
        private string input;
        
        private bool isBarcodeShown = false;
        private bool ignoreInput = false;
        private Animator AN;
        private Bean bean;

        // Start is called before the first frame update
        void Start()
        {
            AN = GetComponent<Animator>();
            bean = GetComponent<Bean>();
            computer.UpdateDisplayText(defaultInputText);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.inputString.Length > 0 && !ignoreInput) {
                char key = Input.inputString[0];

                if (char.IsDigit(key)) {
                    input += key;

                    if (input.Length == barcode.Length) {
                        if (input == barcode) {
                            StartCoroutine(SuccessSequence());
                            ignoreInput = true;
                        } else {
                            StartCoroutine(RetrySequence());
                            ignoreInput = true;
                        }
                    }
                } else if (key == '\b' && input.Length >= 1) {
                    input = input.Remove(input.Length - 1);
                }

                string displayText = "";

                for (int i = 0; i < input.Length; i++) {
                    if (i < input.Length - 1)
                        displayText += input[i] + " ";
                    else
                        displayText += input[i];
                }

                computer.UpdateDisplayText(displayText);
            }
        }

        private IEnumerator RetrySequence() {
            yield return new WaitForSeconds(0.5f);

            yield return StartCoroutine(computer.FlashingText("INCORRECT!", 3, 0.5f));
            yield return StartCoroutine(computer.FlashingText("TRY AGAIN!", 3, 0.5f));

            ResetInput();
        }

        private IEnumerator SuccessSequence() {
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(computer.FlashingText("CORRECT!", 3, 0.5f));

            if (isBarcodeShown) {
                HideBarCode();
                isBarcodeShown = false;
            }

            computer.UpdateDisplayText("DRAG ACROSS");
            bean.SetCurrentState(Bean.State.DRAG);
            this.enabled = false;
        }

        private void ResetInput() {
            input = "";
            computer.UpdateDisplayText(defaultInputText);
            ignoreInput = false;
        }

        private void OnMouseDown() {
            if (!isBarcodeShown && bean.currentState == Bean.State.BARCODE) {
                ShowBarCode();
                isBarcodeShown = true;
            } else if (bean.currentState == Bean.State.BARCODE) {
                HideBarCode();
                isBarcodeShown = false;
            }
        }

        private void ShowBarCode() {
            AN.Play("ShowBarCode");
        }

        private void HideBarCode() {
            AN.Play("HideBarCode");
        }
    }
}
