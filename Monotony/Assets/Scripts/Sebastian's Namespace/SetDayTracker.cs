using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SebastiansNamespace;

public class SetDayTracker : MonoBehaviour
{
    public GameObject dayTracker;
    // Start is called before the first frame update
    void Start()
    {
        dayTracker.GetComponent<TextMeshProUGUI>().text = ("Day " + SavePlayerData.day);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
