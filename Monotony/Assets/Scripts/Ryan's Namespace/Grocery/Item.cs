namespace RyansNamespace
{    public class Item : Drag
    {
        public enum State
        {
            typingBarcode,
            scanning
        }

        private State currentState = State.typingBarcode;
        private Barcode barcode;
        public Customer customer { get; private set; }

        private bool isBarcodeShown = false;
        private bool isScanning;

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

        public override void OnMouseUp()
        {
            if (currentState == State.scanning)
                base.OnMouseUp();
        }

        public void SetState(State state) {
            switch (state) {
                case State.scanning:
                    if (isBarcodeShown) {
                        barcode.Hide();
                        isBarcodeShown = false;
                    }

                    currentState = State.scanning;
                    break;
            }
        }
    }
}