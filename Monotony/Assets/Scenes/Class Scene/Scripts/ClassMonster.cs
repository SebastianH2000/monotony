using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SebastiansNamespace {
    public class ClassMonster : MonoBehaviour
    {
        private bool isMonster = true;
        private float monsterTimer = 0;
        private float monsterTarget = 0;

        public bool autoRun = true;
        // Start is called before the first frame update
        void Start()
        {
            monsterTarget = 0;
            hide();
        }

        // Update is called once per frame
        void Update()
        {
            if (autoRun) {
                if (monsterTimer > monsterTarget) {
                    monsterTimer = 0;
                    isMonster = !isMonster;
                    if (isMonster) {
                        monsterTarget = Random.Range(2f,5f);
                        show();
                    }
                    else {
                        monsterTarget = Random.Range(5f,15f);
                        hide();
                    }
                }
                else {
                    monsterTimer += Time.deltaTime;
                }
            }
        }

        public void show() {
            this.GetComponent<Alpha>().setAlpha(1f);
            this.transform.Find("Monster Back").gameObject.GetComponent<Collider2D>().enabled = true;
        }

        public void hide() {
            this.GetComponent<Alpha>().setAlpha(0f);
            this.transform.Find("Monster Back").gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }
}
