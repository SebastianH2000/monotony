using UnityEngine;

namespace RyansNamespace {
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Drag : MonoBehaviour
    {
        [Header("Gravity")]
        [SerializeField] private bool simulateGravity = false;
        [SerializeField] private float gravity;
        [SerializeField] private float terminalVelocity;
        private float velocity;

        protected Rigidbody2D RB { get; private set; }
        protected BoxCollider2D boxCollider { get; private set; }
        protected Vector2 clampedPos { get; private set; }
        private Vector3 mousePos;
        private float offset;

        private bool isDragging = false;

        protected float xMin;
        protected float xMax;
        protected float yMin;
        protected float yMax;

        public virtual void Awake() {
            RB = GetComponent<Rigidbody2D>();
            boxCollider = GetComponent<BoxCollider2D>();

            clampedPos = RB.position;
        }

        // Start is called before the first frame update
        public virtual void Start()
        {
            RB.bodyType = RigidbodyType2D.Kinematic;

            offset = Camera.main.transform.position.z;

            xMin = Boundaries.instance.GetXMin() + boxCollider.bounds.size.x / 2f;
            xMax = Boundaries.instance.GetXMax() - boxCollider.bounds.size.x / 2f;
            yMin = Boundaries.instance.GetYMin() + boxCollider.bounds.size.y / 2f;
            yMax = Boundaries.instance.GetYMax() - boxCollider.bounds.size.x / 2f;
        }

        // Update is called once per frame
        public virtual void Update()
        {
            if (isDragging)
                mousePos = Input.mousePosition;
        }

        public virtual void FixedUpdate() {
            if (isDragging) {
                mousePos.z = -offset;
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
                clampedPos = new Vector3(Mathf.Clamp(worldPos.x, xMin, xMax), Mathf.Clamp(worldPos.y, yMin, yMax), worldPos.z);
                RB.MovePosition(clampedPos);
            } else if (simulateGravity) {
                velocity += gravity * Time.fixedDeltaTime;
                velocity = Mathf.Clamp(velocity, terminalVelocity, float.MaxValue);

                clampedPos = new Vector3(RB.position.x,
                Mathf.Clamp(RB.position.y + velocity * Time.fixedDeltaTime, yMin, yMax));

                RB.MovePosition(clampedPos);
            }
        }

        public virtual void OnMouseDown() {
            isDragging = true;
            if (simulateGravity)
                velocity = 0f;
        }

        public virtual void OnMouseUp() => isDragging = false;
    }
}
