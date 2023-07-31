using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RyansNamespace {
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Barcode))]
    public class Scan : MonoBehaviour
    {
        [SerializeField] private Computer computer;
        private BoxCollider2D boxCollider;
        private Barcode barcode;
        private bool isScanning = false;

        // Start is called before the first frame update
        void Start()
        {
            boxCollider = GetComponent<BoxCollider2D>();
            barcode = GetComponent<Barcode>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Scanner")) {
                float otherRelativeYPos = other.transform.position.y - other.bounds.size.y / 2f;
                float relativeYPos = transform.position.y - boxCollider.bounds.size.y / 2f;
                if (Mathf.Abs(otherRelativeYPos - relativeYPos) < 0.1f && transform.position.x > other.transform.position.x) {
                    isScanning = true;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.CompareTag("Scanner")) {
                if (isScanning) {
                    float otherRelativeYPos = other.transform.position.y - other.bounds.size.y / 2f;
                    float relativeYPos = transform.position.y - boxCollider.bounds.size.y / 2f;
                    if (Mathf.Abs(otherRelativeYPos - relativeYPos) < 0.1f && transform.position.x < other.transform.position.x) {
                        isScanning = false;
                        StartCoroutine(computer.FlashingText("SCANNED!", 3, 0.5f));
                    } else {
                        isScanning = false;
                        StartCoroutine(computer.FlashingText("SCAN FAILED", 3, 0.5f));
                    }
                }
            }
        }
    }
}
