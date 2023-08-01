using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GettingReady {
    [RequireComponent(typeof(BoxCollider2D))]
    public class Toothbrush : MonoBehaviour
    {
        private enum State {
            Null,
            MovingLeft,
            MovingRight
        }

        [SerializeField] private float minX;
        [SerializeField] private float maxX;
        [SerializeField] private float threshold;
        [SerializeField] private int timesToBrush;

        private bool isDragging = false;
        private int timesBrushed = 0;
        private float distanceDragged = 0f;
        private State currentState = State.Null;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (isDragging) {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = -Camera.main.transform.position.z;

                Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
                Vector3 clampedPos = new Vector3(Mathf.Clamp(worldPos.x, minX, maxX),
                transform.position.y, transform.position.z);

                if (clampedPos.x > transform.position.x) {
                    switch (currentState) {
                        case State.MovingLeft:
                            if (distanceDragged > threshold) {
                                timesBrushed++;

                                if (timesBrushed >= timesToBrush) {
                                    Debug.Log("You brushed your teeth!");
                                }
                            }

                            currentState = State.MovingRight;
                            distanceDragged = 0f;
                            break;
                        case State.MovingRight:
                            distanceDragged += Mathf.Abs(clampedPos.x - transform.position.x);
                            break;
                        case State.Null:
                            currentState = State.MovingRight;
                            break;
                    }
                } else if (clampedPos.x < transform.position.x) {
                    switch (currentState) {
                        case State.MovingRight:
                            if (distanceDragged > threshold) {
                                timesBrushed++;

                                if (timesBrushed >= timesToBrush) {
                                    Debug.Log("You brushed your teeth!");
                                }
                            }

                            currentState = State.MovingLeft;
                            distanceDragged = 0f;
                            break;
                        case State.MovingLeft:
                            distanceDragged += Mathf.Abs(clampedPos.x - transform.position.x);
                            break;
                        case State.Null:
                            currentState = State.MovingLeft;
                            break;
                    }
                }

                transform.position = clampedPos;
            }            
        }

        private void OnMouseDown() {
            isDragging = true;
        }

        private void OnMouseUp() {
            isDragging = false;
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(minX, transform.position.y, transform.position.z),
            new Vector3(maxX, transform.position.y, transform.position.z));
        }
    }
}
