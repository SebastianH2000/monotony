using System.Collections;
using UnityEngine;

namespace RyansNamespace {
    [RequireComponent(typeof(BoxCollider2D))]
    public class Scanner : MonoBehaviour
    {
        private BoxCollider2D boxCollider;
        private bool isScanning = false;

        private void Awake() {
            boxCollider = GetComponent<BoxCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Item"))
            {
                if (transform.position.x + boxCollider.bounds.size.x / 2f < other.transform.position.x)
                    isScanning = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Item"))
            {
                if (isScanning)
                {
                    if (transform.position.x - boxCollider.bounds.size.x / 2f > other.transform.position.x) {
                        StartCoroutine(Success(other.gameObject));
                    } else {
                        StartCoroutine(Display.instance.Example(Display.State.scan, false));
                    }

                    isScanning = false;
                }
            }
        }

        private IEnumerator Success(GameObject other) {
            yield return StartCoroutine(Display.instance.Example(Display.State.scan, true));
            Destroy(other);
            other.GetComponent<Item>().customer.SpawnItem();
        }
    }
}
