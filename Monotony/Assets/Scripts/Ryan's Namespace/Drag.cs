using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RyansNamespace {
    [RequireComponent(typeof(Animator))]
    public class Drag : MonoBehaviour
    {
        private enum State {
            SCANNING,
            DRAGGING
        }

        [Header("Bar Code")]
        [SerializeField] private TextMeshProUGUI inputText;
        [SerializeField] private string barCode;
        private string input;

        [Header("Boundaries")]
        [SerializeField] private float minX;
        [SerializeField] private float maxX;
        [SerializeField] private float minY;
        [SerializeField] private float maxY;

        [Header("Gravity")]
        [SerializeField] private float gravity;
        [SerializeField] private float terminalVelocity;
        private float velocity = 0f;

        private bool isDragging = false;
        private bool isBarCodeShown = false;

        private State currentState = State.SCANNING;
        private Animator AN;

        // Start is called before the first frame update
        void Start()
        {
            BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
            AN = GetComponent<Animator>();
            inputText.text = "";

            minX += boxCollider.bounds.size.x / 2f;
            maxX -= boxCollider.bounds.size.x / 2f;
            minY += boxCollider.bounds.size.y / 2f;
            maxY -= boxCollider.bounds.size.y / 2f;
        }

        // Update is called once per frame
        void Update()
        {
            if (currentState == State.SCANNING) {
                if (Input.inputString.Length > 0 && char.IsDigit(Input.inputString[0])) {
                    char key = Input.inputString[0];
                    input += key;

                    if (input.Length == barCode.Length) {
                        if (input == barCode) {
                            Debug.Log("you did it!");
                        } else {
                            Debug.Log("you're dumb");
                            input = "";
                        }
                    }
                } else if (Input.GetKeyDown(KeyCode.Backspace) && input.Length >= 1) {
                    input = input.Remove(input.Length - 1);
                }

                inputText.text = input;
            }

            if (isDragging) {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = -Camera.main.transform.position.z;
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
                Vector3 clampedPos = new Vector3(Mathf.Clamp(worldPos.x, minX, maxX), Mathf.Clamp(worldPos.y, minY, maxY), worldPos.z);

                transform.position = clampedPos;
            } else {
                velocity += gravity * Time.deltaTime;
                velocity = Mathf.Clamp(velocity, terminalVelocity, float.MaxValue);

                Vector3 clampedPos = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX),
                Mathf.Clamp(transform.position.y + velocity * Time.deltaTime, minY, maxY),
                transform.position.z);

                transform.position = clampedPos;
            }
        }

        private void OnMouseDown() {
            if (currentState == State.SCANNING) {
                if (!isBarCodeShown) {
                    ShowBarCode();
                    isBarCodeShown = true;
                } else {
                    HideBarCode();
                    isBarCodeShown = false;
                }
            } else if (currentState == State.DRAGGING) {
                isDragging = true;
                velocity = 0f;
            }
        }

        private void OnMouseUp() {
            if (currentState == State.DRAGGING) {
                isDragging = false;
            }
        }

        private void ShowBarCode() {
            AN.Play("ShowBarCode");
        }

        private void HideBarCode() {
            AN.Play("HideBarCode");
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(new Vector3((minX + maxX) / 2f, (minY + maxY) / 2f, 0f), new Vector3(maxX - minX, maxY - minY, 0f));
        }
    }
}
