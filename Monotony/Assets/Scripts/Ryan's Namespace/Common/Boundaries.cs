using UnityEngine;

namespace RyansNamespace {
    public class Boundaries : MonoBehaviour
    {
        public static Boundaries instance { get; private set; }

        [SerializeField] private float xMin, xMax, yMin, yMax;

        private void Awake() {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            Vector2 minPos = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, -Camera.main.transform.position.z));
            Vector2 maxPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, -Camera.main.transform.position.z));

            xMin = Mathf.Clamp(xMin, minPos.x, maxPos.x);
            xMax = Mathf.Clamp(xMax, minPos.x, maxPos.x);
            yMin = Mathf.Clamp(yMin, minPos.y, maxPos.y);
            yMax = Mathf.Clamp(yMax, minPos.y, maxPos.y);
        }

        public float GetXMin() {
            return xMin;
        }

        public float GetXMax() {
            return xMax;
        }

        public float GetYMin() {
            return yMin;
        }

        public float GetYMax() {
            return yMax;
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector2(xMin, yMin), new Vector2(xMin, yMax));
            Gizmos.DrawLine(new Vector2(xMin, yMax), new Vector2(xMax, yMax));
            Gizmos.DrawLine(new Vector2(xMax, yMax), new Vector2(xMax, yMin));
            Gizmos.DrawLine(new Vector2(xMax, yMin), new Vector2(xMin, yMin));
        }
    }
}
