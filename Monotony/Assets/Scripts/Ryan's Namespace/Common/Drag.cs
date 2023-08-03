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

        private Rigidbody2D RB;
        public BoxCollider2D boxCollider { get; private set; }
        private Vector3 mousePos;
        private float offset;

        private bool isDragging = false;

        private float xMin;
        private float xMax;
        private float yMin;
        private float yMax;

        public virtual void Awake() {
            RB = GetComponent<Rigidbody2D>();
            boxCollider = GetComponent<BoxCollider2D>();
        }

        // Start is called before the first frame update
        public virtual void Start()
        {
            RB.bodyType = RigidbodyType2D.Kinematic;

            offset = Camera.main.transform.position.z;

            xMin = Boundaries.instance.GetXMin();
            xMax = Boundaries.instance.GetXMax();
            yMin = Boundaries.instance.GetYMin();
            yMax = Boundaries.instance.GetYMax();

            xMin += boxCollider.bounds.size.x / 2f;
            xMax -= boxCollider.bounds.size.x / 2f;
            yMin += boxCollider.bounds.size.y / 2f;
            yMax -= boxCollider.bounds.size.y / 2f;
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
                Vector3 clampedPos = new Vector3(Mathf.Clamp(worldPos.x, xMin, xMax), Mathf.Clamp(worldPos.y, yMin, yMax), worldPos.z);
                RB.MovePosition(clampedPos);
            } else if (simulateGravity) {
                velocity += gravity * Time.fixedDeltaTime;
                velocity = Mathf.Clamp(velocity, terminalVelocity, float.MaxValue);

                Vector2 clampedPos = new Vector3(Mathf.Clamp(RB.position.x, xMin, xMax),
                Mathf.Clamp(RB.position.y + velocity * Time.fixedDeltaTime, yMin, yMax));

                RB.MovePosition(clampedPos);
            }
        }

        public virtual void OnMouseDown() {
            isDragging = true;
            if (simulateGravity)
                velocity = 0f;
        }

        public virtual void OnMouseUp() {
            isDragging = false;
        }

        public virtual void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(new Vector3((xMin + xMax) / 2f, (yMin + yMax) / 2f, 0f), new Vector3(xMax - xMin, yMax - yMin, 0f));
        }
    }
}
