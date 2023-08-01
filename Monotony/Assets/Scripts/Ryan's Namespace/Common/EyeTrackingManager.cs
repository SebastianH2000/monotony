using UnityEngine;
using Tobii.Gaming;

namespace RyansNamespace {
    public class EyeTrackingManager : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private float damage = 10f;

        private float offset;
        private bool isConnected = false;

        // Start is called before the first frame update
        void Start()
        {
            if (TobiiAPI.IsConnected)
                isConnected = true;
            else
                isConnected = false;

            offset = Camera.main.transform.position.z;
        }

        // Update is called once per frame
        void Update()
        {
            if (!isConnected || TobiiAPI.GetUserPresence() != UserPresence.Present)
                return;

            GazePoint gazePoint = TobiiAPI.GetGazePoint();
            if (gazePoint.IsValid)
            {
                Vector3 screenPos = gazePoint.Screen;
                screenPos.z = -offset;
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

                player.transform.position = worldPos;
                Collider2D collider = Physics2D.Raycast(worldPos, Vector2.zero).collider;

                if (collider != null) {
                    if (collider.CompareTag("Monster"))
                        player.TakeDamage(damage * Time.deltaTime);
                }
            }
        }
    }
}
