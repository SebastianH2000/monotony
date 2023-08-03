using System.Collections;
using UnityEngine;
using TMPro;

namespace RyansNamespace {
    [RequireComponent(typeof(SpriteRenderer))]
    public class Barcode : MonoBehaviour
    {
        [Header("Animation")]
        [SerializeField] private float scaleTime;
        [SerializeField] private float alphaTime;
        [SerializeField] private Transform hideTransform;
        [SerializeField] private Transform showTransform;
        private float scaleStopwatch;
        private float alphaStopwatch;

        [Header("Barcode")]
        [SerializeField] private TextMeshProUGUI barcodeText;
        [SerializeField] private int barcodeLength;
        public string barcode { get; private set; } = "";

        private SpriteRenderer SR;
        private Coroutine currentCoroutine;

        private void Awake() => SR = GetComponent<SpriteRenderer>();

        // Start is called before the first frame update
        void Start()
        {
            GenerateRandomBarcode();
            barcodeText.text = "";

            for (int i = 0; i < barcode.Length; i++) {
                if (i < barcode.Length - 1)
                    barcodeText.text += barcode[i] + " ";
                else
                    barcodeText.text += barcode[i];
            }

            Display.instance.SetBarcode(barcode, transform.parent.GetComponent<Item>());

            scaleStopwatch = scaleTime;
            alphaStopwatch = alphaTime;
        }

        private void GenerateRandomBarcode() {
            for (int i = 0; i < barcodeLength; i++)
                barcode += Random.Range(0, 10);
        }

        public void Show() => currentCoroutine = StartCoroutine(Animate(true));

        public void Hide() => currentCoroutine = StartCoroutine(Animate(false));

        public IEnumerator Animate(bool show) {
            if (currentCoroutine != null)
                StopCoroutine(currentCoroutine);

            scaleStopwatch = scaleTime - scaleStopwatch;
            alphaStopwatch = alphaTime - alphaStopwatch;

            while (scaleStopwatch <= scaleTime || alphaStopwatch <= alphaTime) {
                if (scaleStopwatch <= scaleTime) {
                    scaleStopwatch += Time.deltaTime;

                    transform.localScale = Vector3.Lerp(show ? hideTransform.localScale : showTransform.localScale,
                    show ? showTransform.localScale : hideTransform.localScale,
                    scaleStopwatch / scaleTime);

                    transform.localPosition = Vector3.Lerp(show ? hideTransform.localPosition : showTransform.localPosition,
                    show ? showTransform.localPosition : hideTransform.localPosition,
                    scaleStopwatch / scaleTime);
                }

                if (alphaStopwatch <= alphaTime) {
                    if (show) {
                        alphaStopwatch += Time.deltaTime;
                        SR.color = new Color(SR.color.r, SR.color.g, SR.color.b,
                        Mathf.Lerp(0f, 1f, alphaStopwatch / alphaTime));
                    } else if (scaleStopwatch > scaleTime - alphaTime) {
                        alphaStopwatch += Time.deltaTime;
                        SR.color = new Color(SR.color.r, SR.color.g, SR.color.b,
                        Mathf.Lerp(1f, 0f, alphaStopwatch / alphaTime));
                    }
                }
                
                yield return null;
            }

            scaleStopwatch = scaleTime;
            alphaStopwatch = alphaTime;
        }
    }
}
