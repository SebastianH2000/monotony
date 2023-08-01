using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class EyeTrackingManager : MonoBehaviour
{
    private float offset;
    private bool isConnected = false;

    // Start is called before the first frame update
    void Start()
    {
        if (TobiiAPI.IsConnected)
        {
            Debug.Log("Eye Tracker is connected");
            isConnected = true;
        }
        else
        {
            Debug.Log("Eye Tracker is not connected");
            isConnected = false;
        }

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
            Debug.Log("Gaze point is valid");
            Debug.Log("Gaze point is: " + gazePoint.Screen);

            Vector3 screenPos = gazePoint.Screen;
            screenPos.z = -offset;
            Debug.Log("Gaze point in world space is: " + Camera.main.ScreenToWorldPoint(screenPos));
        }
    }
}
