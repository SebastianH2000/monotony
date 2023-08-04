using System.Collections;
using UnityEngine;
using TMPro;

namespace RyansNamespace {
    public class Display : MonoBehaviour
    {
        public static Display instance { get; private set; }

        public enum State {
            typingBarcode,
            scanning,
            scanned
        }

        [Header("References")]
        [SerializeField] private TextMeshProUGUI displayText;

        [Header("Barcode")]
        [SerializeField] private string barcodePrompt;
        [SerializeField] private string barcodeSuccessText;
        [SerializeField] private string barcodeFailureText;
        
        private Item currentItem = null;
        private bool ignoreInput = false;
        private string barcode = "";
        private string input = "";

        [Header("Scan")]
        [SerializeField] private string scanPrompt;
        [SerializeField] private string scanSuccessText;
        [SerializeField] private string scanFailureText;

        // Start is called before the first frame update
        void Start()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.inputString.Length > 0 && !ignoreInput)
            {
                char key = Input.inputString[0];

                if (char.IsDigit(key))
                {
                    input += key;

                    if (input.Length == barcode.Length)
                    {
                        if (input == barcode)
                            StartCoroutine(Handle(State.typingBarcode, true));
                        else
                            StartCoroutine(Handle(State.typingBarcode, false));

                        ignoreInput = true;
                    }
                } else if (key == '\b' && input.Length >= 1)
                {
                    input = input.Remove(input.Length - 1);
                } else {
                    return;
                }

                AppManager.instance.sfxManager.PlaySFX("key_" + Random.Range(1, 4).ToString(), 1f);

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

        public IEnumerator Handle(State state, bool success) {        
            switch (state) {
                case State.typingBarcode:
                    yield return new WaitForSeconds(0.5f);
                    yield return StartCoroutine(FlashingText(success ? barcodeSuccessText : barcodeFailureText, 3, 0.5f));

                    input = "";
                    displayText.text = success ? scanPrompt : barcodePrompt;
                    ignoreInput = false;

                    if (success) {
                        currentItem.SetState(Item.State.scanning);
                        barcode = "";
                        currentItem = null;
                    }

                    break;
                case State.scanning:
                    yield return StartCoroutine(FlashingText(success ? scanSuccessText : scanFailureText, 3, 0.5f)); 
                    break;
            }
        }

        private IEnumerator FlashingText(string text, int times, float delay) {
            for (int i = 0; i < times; i++) {
                displayText.text = text;
                yield return new WaitForSeconds(delay);
                displayText.text = "";
                yield return new WaitForSeconds(delay);
            }
        }

        public void SetBarcode(string barcode, Item currentItem) {
            this.barcode = barcode;
            this.currentItem = currentItem;
            ignoreInput = false;
            displayText.text = barcodePrompt;
        }
    }
}
