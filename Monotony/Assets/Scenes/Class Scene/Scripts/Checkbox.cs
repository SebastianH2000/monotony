using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SebastiansNamespace {
    public class Checkbox : MonoBehaviour
    {
        public Sprite[] checkboxSprites = new Sprite[2];

        // Start is called before the first frame update
        void Start()
        {
            this.GetComponent<SpriteRenderer>().sprite = checkboxSprites[0];
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void Off() {
            this.GetComponent<SpriteRenderer>().sprite = checkboxSprites[0];
        }

        public void On() {
            this.GetComponent<SpriteRenderer>().sprite = checkboxSprites[1];
        }
    }
}
