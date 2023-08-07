using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] spriteArray = new Sprite[1];
    public bool canSetSprite;
    void Start()
    {
        this.transform.parent.transform.position = new Vector3(Random.Range(-9f,6.5f),Random.Range(-3f,5f)-2,(-4 - Random.Range(0f,0.01f)));
        if (canSetSprite) {
            this.GetComponent<SpriteRenderer>().sprite = spriteArray[Random.Range(0, spriteArray.Length)];
        }
        Transform[] children = this.transform.parent.GetComponentsInChildren<Transform>();
        for (int i = 0; i < children.Length; i++) {
            //children[i].localPosition += this.transform.localPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
