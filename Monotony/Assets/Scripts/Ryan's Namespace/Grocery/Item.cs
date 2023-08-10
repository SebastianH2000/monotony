 using UnityEngine;

namespace RyansNamespace
{    public class Item : Drag
    {
        public enum State
        {
            typingBarcode,
            scanning,
            scanned,
            bagging,
            bagged
        }

        private State currentState = State.typingBarcode;
        private Barcode barcode;
        private Customer customer;

        private bool isBarcodeShown = false;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            barcode = GetComponentInChildren<Barcode>();
        }

        public void SetUp(Customer customer) => this.customer = customer;

        public State GetState() => currentState;

        protected override void OnMouseDown()
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
                    AppManager.instance.sfxManager.PlaySFX("mouse_click", 1f);
                    break;
                case State.scanned:
                case State.scanning:
                    base.OnMouseDown();
                    AppManager.instance.sfxManager.PlaySFX("item_pick_up", 1f);
                    break;
            }
        }

        protected override void FixedUpdate() {
            base.FixedUpdate();

            if (currentState == State.bagging) {
                RB.MovePosition(RB.position + Vector2.left * Time.fixedDeltaTime * 3f);
            }
        }

        public void SetState(State state) {
            switch (state) {
                case State.scanned:
                case State.scanning:
                    if (isBarcodeShown) {
                        barcode.Hide();
                        isBarcodeShown = false;
                        AppManager.instance.sfxManager.PlaySFX("mouse_click", 1f);
                    }
                    break;
                case State.bagging:
                    base.OnMouseUp();
                    Debug.Log(gameObject.name + "bagging now");
                    break;
                case State.bagged:
                    customer.SpawnItem();
                    AppManager.instance.sfxManager.PlaySFX("grocery_bagging", 1f);
                    Destroy(gameObject);
                    break;
            }

            currentState = state;
        }
    }
}