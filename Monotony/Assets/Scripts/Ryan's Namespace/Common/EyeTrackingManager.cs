using UnityEngine;
using Tobii.Gaming;
using SebastiansNamespace;

namespace RyansNamespace {
    public class EyeTrackingManager : MonoBehaviour
    {
        private float offset;
        private bool isConnected = false;
        [SerializeField] private Transform visionLocation;
        [SerializeField] private float checkRadius;
        private bool inEditor = false;

        // Start is called before the first frame update
        void Start()
        {
            offset = Camera.main.transform.position.z;

            if (Application.isEditor) {
                inEditor = true;
                return;
            }

            if (TobiiAPI.IsConnected)
                isConnected = true;
            else
                isConnected = false;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 screenPos;

            if (inEditor) {
                screenPos = Input.mousePosition;
            } else {
                if (!isConnected || TobiiAPI.GetUserPresence() != UserPresence.Present)
                    return;

                GazePoint gazePoint = TobiiAPI.GetGazePoint();
                if (gazePoint.IsValid) {
                    screenPos = gazePoint.Screen;
                } else {
                    screenPos = Input.mousePosition;
                }
            }

            screenPos.z = -offset;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            visionLocation.position = new Vector3(worldPos.x, worldPos.y, 0f);

            int inverseDistance = 0;

            for (int i = inverseDistance; i < 5; i++) {
                Collider2D collider = Physics2D.OverlapCircle(worldPos, checkRadius / Mathf.Pow(2, i));

                if (collider != null) {
                    if (collider.CompareTag("Monster")) {
                        if (i > inverseDistance)
                            inverseDistance = i;
                            SavePlayerData.monsterDistance = i;
                    }
                }
            }

            if (inverseDistance > 0) {
                SavePlayerData.sanity = Mathf.Clamp(SavePlayerData.sanity - (Time.deltaTime / 30f * (inverseDistance)), 0f, 1f);
                if (SavePlayerData.sanity <= 0)
                    GameObject.Find("FadeOut").GetComponent<FadeOut>().isFading = true;
                SavePlayerData.lookingAtMonster = true;
                return;
            }

            SavePlayerData.lookingAtMonster = false;
            SavePlayerData.monsterDistance = 0;
        }
    }
}
