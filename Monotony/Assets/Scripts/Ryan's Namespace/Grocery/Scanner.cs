using UnityEngine;

namespace RyansNamespace {
    [RequireComponent(typeof(BoxCollider2D))]
    public class Scanner : MonoBehaviour
    {
        private BoxCollider2D boxCollider;
        private bool isScanning = false;

        private void Awake() => boxCollider = GetComponent<BoxCollider2D>();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Item"))
            {
                // If the item is to the right of the scanner, it is being scanned
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
                    isScanning = false;
                    // If the item is to the left of the scanner, it has been scanned
                    if (transform.position.x - boxCollider.bounds.size.x / 2f > other.transform.position.x) {
                        StartCoroutine(other.GetComponent<Item>().HandleSuccessfulScan());
                        return;
                    }
                }

                other.GetComponent<Item>().HandleFailedScan();
            }
        }
    }
}
