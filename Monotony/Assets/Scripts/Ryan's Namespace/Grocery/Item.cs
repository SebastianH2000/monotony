using System.Collections;
using UnityEditor.Callbacks;
using UnityEngine;

namespace RyansNamespace
{    public class Item : Drag
    {
        public enum State
        {
            typingBarcode,
            scanning,
            scanned
        }

        private State currentState = State.typingBarcode;
        private Barcode barcode;
        private Customer customer;

        private bool isBarcodeShown = false;

        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
            barcode = GetComponentInChildren<Barcode>();
        }

        public void SetUp(Customer customer) => this.customer = customer;

        public override void OnMouseDown()
        {
            switch (currentState)
            {
                case State.typingBarcode:
                    if (!isBarcodeShown)
                    {
                        barcode.Show();
                        isBarcodeShown = true;
                    }
                    else
                    {
                        barcode.Hide();
                        isBarcodeShown = false;
                    }
                    break;
                case State.scanning:
                    base.OnMouseDown();
                    break;
            }
        }

        public override void FixedUpdate() {
            base.FixedUpdate();

            if (currentState == State.scanned) {
                base.RB.MovePosition(base.RB.position + Vector2.left * Time.fixedDeltaTime * 3f);

                if (RB.position.x < Boundaries.instance.GetXMin() - boxCollider.bounds.size.x / 2f) {
                    customer.SpawnItem();
                    Destroy(gameObject);
                }
            }
        }

        public IEnumerator HandleSuccessfulScan() {
            yield return StartCoroutine(Display.instance.Handle(Display.State.scanning, true));
            SetState(State.scanned);
        }

        public void HandleFailedScan() {
            StartCoroutine(Display.instance.Handle(Display.State.scanning, false));
        }

        public void SetState(State state) {
            if (state == State.scanning && isBarcodeShown) {
                barcode.Hide();
                isBarcodeShown = false;
            }

            currentState = state;
        }
    }
}