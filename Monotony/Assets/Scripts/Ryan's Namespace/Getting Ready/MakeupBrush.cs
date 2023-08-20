using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GettingReady {
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class MakeupBrush : MonoBehaviour
    {
        private bool isDragging = false;
        [SerializeField] private float minX;
        [SerializeField] private float maxX;
        [SerializeField] private float minY;
        [SerializeField] private float maxY;
        [SerializeField] private float speed;

        private Rigidbody2D RB;

        // Start is called before the first frame update
        void Start()
        {
            RB = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            if (isDragging) {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = -Camera.main.transform.position.z;

                Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
                Vector3 clampedPos = new Vector3(Mathf.Clamp(worldPos.x, minX, maxX),
                Mathf.Clamp(worldPos.y, minY, maxY), transform.position.z);

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
            Gizmos.DrawWireCube(new Vector3((minX + maxX) / 2f, (minY + maxY) / 2f, 0f), new Vector3(maxX - minX, maxY - minY, 0f));
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                StartCoroutine(MoveTowards(other.transform.position));
            }
        }

        private IEnumerator MoveTowards(Vector3 target) {
            while (Vector3.Distance(transform.position, target) > 0.1f) {
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                yield return null;
            }

            transform.position = target;
        }
    }
}
