using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SebastiansNamespace {
    public class GettingReadyMonster : MonoBehaviour
    {
        private bool isMonster = true;
        private float monsterTimer = 0;
        private float monsterTarget = 0;
        public GameObject toggleObject;

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
                        monsterTarget = Random.Range(4f,6f);
                        show();
                    }
                    else {
                        monsterTarget = Random.Range(8f,15f);
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
            Debug.Log(toggleObject);
            if (toggleObject != null) {
                toggleObject.GetComponent<Alpha>().setAlpha(0f);
                toggleObject.GetComponent<Collider2D>().enabled = false;
            }
        }

        public void hide() {
            this.GetComponent<Alpha>().setAlpha(0f);
            this.transform.Find("Monster Back").gameObject.GetComponent<Collider2D>().enabled = false;
            if (toggleObject != null) {
                toggleObject.GetComponent<Alpha>().setAlpha(1f);
                toggleObject.GetComponent<Collider2D>().enabled = true;
            }
        }
    }
}
