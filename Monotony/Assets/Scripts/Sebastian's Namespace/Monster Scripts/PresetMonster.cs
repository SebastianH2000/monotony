using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SebastiansNamespace {
    public class PresetMonster : MonoBehaviour
    {
        private float waitingTimer;
        private float waitingTarget;
        // Start is called before the first frame update
        void Start()
        {
            waitingTarget = Random.Range(0f,1.2f);
            this.transform.Find("Monster Part").gameObject.GetComponent<SpriteRenderer>().sortingOrder = this.transform.parent.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
        }

        // Update is called once per frame
        void Update()
        {
            if (waitingTimer > waitingTarget) {
                this.transform.Find("Monster Part").gameObject.GetComponent<Animator>().SetBool("Running",true);
            }
            else {
                waitingTimer += Time.deltaTime;
            }
        }
    }
}
