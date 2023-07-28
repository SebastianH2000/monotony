using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RyansNamespace {
    public class Drag : MonoBehaviour
    {
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

        // Start is called before the first frame update
        void Start()
        {
            BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();

            minX += boxCollider.bounds.size.x / 2f;
            maxX -= boxCollider.bounds.size.x / 2f;
            minY += boxCollider.bounds.size.y / 2f;
            maxY -= boxCollider.bounds.size.y / 2f;
        }

        // Update is called once per frame
        void Update()
        {
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
            isDragging = true;
            velocity = 0f;
        }

        private void OnMouseUp() {
            isDragging = false;
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(new Vector3((minX + maxX) / 2f, (minY + maxY) / 2f, 0f), new Vector3(maxX - minX, maxY - minY, 0f));
        }
    }
}
