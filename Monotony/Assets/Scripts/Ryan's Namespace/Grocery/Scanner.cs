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
                // If the item is to the right of the scanner and the item is in the scanning state, it is being scanned
                if (transform.position.x + boxCollider.bounds.size.x / 2f < other.transform.position.x &&
                other.GetComponent<Item>().GetState() == Item.State.scanning)
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
                        AppManager.instance.sfxManager.PlaySFX("item_scanned", 1f);
                        other.GetComponent<Item>().SetState(Item.State.scanned);
                        StartCoroutine(Display.instance.Handle(Display.State.scanning, true));
                        return;
                    }
                }

                AppManager.instance.sfxManager.PlaySFX("item_scan_fail", 1f);
                StartCoroutine(Display.instance.Handle(Display.State.scanning, false));
            }
        }
    }
}
