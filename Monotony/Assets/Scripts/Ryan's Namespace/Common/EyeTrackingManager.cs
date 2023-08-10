using UnityEngine;
using Tobii.Gaming;
using SebastiansNamespace;

namespace RyansNamespace {
    public class EyeTrackingManager : MonoBehaviour
    {
        //[SerializeField] private PlayerHealth player;

        private float offset;
        private bool isConnected = false;
        [SerializeField] private GameObject visionLocation;
        [SerializeField] private float checkRadius; 

        // Start is called before the first frame update
        void Start()
        {
            if (TobiiAPI.IsConnected)
                isConnected = true;
            else
                isConnected = false;

            offset = Camera.main.transform.position.z;
            //visionLocation.transform.localScale = new Vector3(checkRadius,checkRadius,1);
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            visionLocation.transform.position = new Vector3(worldPos.x,worldPos.y,-5);
            if (!isConnected || TobiiAPI.GetUserPresence() != UserPresence.Present)
                //return;

            //GazePoint gazePoint = TobiiAPI.GetGazePoint();
            //if (gazePoint.IsValid && GameObject.Find("IntroCard"))
            if (true && GameObject.Find("IntroCard"))
            {
                //Vector3 screenPos = gazePoint.Screen;
                //screenPos.z = -offset;
                //Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
                worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //visionLocation.transform.position = worldPos;

                //Collider2D collider = Physics2D.Raycast(worldPos, Vector2.zero).collider;

                int returnDist = 0;
                for (int i = 1; i < 5; i++) {
                    Collider2D collider = Physics2D.OverlapCircle(worldPos, checkRadius/Mathf.Pow(2,i));
                    if (collider != null) {
                        if (collider.CompareTag("Monster")) {
                            if (i > returnDist)
                                returnDist = i;
                                SavePlayerData.monsterDistance = i;
                        }
                    }
                }
                if (returnDist > 0) {
                    Debug.Log(returnDist);
                    //float distance = Vector2.Distance(worldPos, collider.transform.positon);
                    SavePlayerData.sanity = Mathf.Clamp(SavePlayerData.sanity - (Time.deltaTime / 30f * (returnDist)), 0f, 1f);
                    SavePlayerData.lookingAtMonster = true;
                    return;
                }

                /*Collider2D collider = Physics2D.OverlapCircle(worldPos, checkRadius);
                if (collider != null) {
                    if (collider.CompareTag("Monster")) {
                        float distance = Vector2.Distance(worldPos, collider.transform.positon);
                        SavePlayerData.sanity = Mathf.Clamp(SavePlayerData.sanity - Time.deltaTime / 5f, 0f, 1f);
                        SavePlayerData.lookingAtMonster = true;
                        return;
                    }
                }*/

                SavePlayerData.lookingAtMonster = false;
                SavePlayerData.monsterDistance = 0;
            }
        }
    }
}
