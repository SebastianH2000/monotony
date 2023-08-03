using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] spriteArray = new Sprite[1];
    void Start()
    {
        this.transform.parent.transform.position = new Vector3(Random.Range(-9,6.5f),Random.Range(-4,4)-2,-4);
        this.GetComponent<SpriteRenderer>().sprite = spriteArray[Random.Range(0, (spriteArray.Length - 1))];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
