using UnityEngine;

namespace RyansNamespace {
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Drag : MonoBehaviour
    {
        [Header("Boundaries")]
        [SerializeField] private float minX;
        [SerializeField] private float maxX;
        [SerializeField] private float minY;
        [SerializeField] private float maxY;

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

        // Start is called before the first frame update
        public virtual void Start()
        {
            RB = GetComponent<Rigidbody2D>();
            RB.bodyType = RigidbodyType2D.Kinematic;

            offset = Camera.main.transform.position.z;

            boxCollider = GetComponent<BoxCollider2D>();
            minX += boxCollider.bounds.size.x / 2f;
            maxX -= boxCollider.bounds.size.x / 2f;
            minY += boxCollider.bounds.size.y / 2f;
            maxY -= boxCollider.bounds.size.y / 2f;
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
                Vector3 clampedPos = new Vector3(Mathf.Clamp(worldPos.x, minX, maxX), Mathf.Clamp(worldPos.y, minY, maxY), worldPos.z);
                RB.MovePosition(clampedPos);
            } else if (simulateGravity) {
                velocity += gravity * Time.fixedDeltaTime;
                velocity = Mathf.Clamp(velocity, terminalVelocity, float.MaxValue);

                Vector2 clampedPos = new Vector3(Mathf.Clamp(RB.position.x, minX, maxX),
                Mathf.Clamp(RB.position.y + velocity * Time.fixedDeltaTime, minY, maxY));

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
            Gizmos.DrawWireCube(new Vector3((minX + maxX) / 2f, (minY + maxY) / 2f, 0f), new Vector3(maxX - minX, maxY - minY, 0f));
        }
    }
}
