using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SebastiansNamespace;

public class Hand : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown() {
        Debug.Log("HAND");
        GameObject.Find("Class Controller").GetComponent<ClassScript>().clickHand();
    }
}
