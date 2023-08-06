using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SebastiansNamespace {
    public class RandomMonster : MonoBehaviour
    {
        public GameObject monster1;
        public GameObject monster2;
        public GameObject monster3;

        private GameObject thisObject;
        // Start is called before the first frame update
        void Start()
        {
            float randomNumber = Random.Range(0f,1f);
            if (randomNumber < 0.33f) {
                GameObject monster1Object = Instantiate(monster1,this.transform);
                thisObject = monster1Object;
                monster1Object.transform.parent = this.transform;
            }
            else if (randomNumber < 0.66f) {
                GameObject monster2Object = Instantiate(monster2,this.transform);
                thisObject = monster2Object;
                monster2Object.transform.parent = this.transform;
            }
            else {
                GameObject monster3Object = Instantiate(monster3,this.transform);
                thisObject = monster3Object;
                monster3Object.transform.parent = this.transform;
            }
            thisObject.transform.position = this.transform.position;
            this.GetComponent<SpriteRenderer>().enabled = false;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
