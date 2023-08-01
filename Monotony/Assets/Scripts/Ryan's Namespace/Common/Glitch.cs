using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RyansNamespace {
    [RequireComponent(typeof(SpriteRenderer))]
    public class Glitch : MonoBehaviour
    {
        [SerializeField] private GameObject[] monsters;
        [Range(0f, 1f)]
        [SerializeField] private float chanceToTurnIntoMonster, chanceToTurnBack;
        [SerializeField] private float timeBetweenPotentialSwitches;
        private float timer;
        private GameObject spawnedMonster = null;
        private SpriteRenderer SR;
        private bool isMonster = false;

        // Start is called before the first frame update
        void Start()
        {
            timer = timeBetweenPotentialSwitches;
            SR = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            if (timer > 0f) {
                timer -= Time.deltaTime;
            } else {
                timer = timeBetweenPotentialSwitches;
                
                if (!isMonster) {
                    if (Random.Range(0f, 1f) < chanceToTurnIntoMonster) {
                        int randomMonster = Random.Range(0, monsters.Length);
                        spawnedMonster = (GameObject)Instantiate(monsters[randomMonster], transform.position, Quaternion.identity);
                        SR.enabled = false;

                        isMonster = true;
                    }
                } else {
                    if (Random.Range(0f, 1f) < chanceToTurnBack) {
                        Destroy(spawnedMonster);
                        SR.enabled = true;

                        isMonster = false;
                    }
                }
            }
        }
    }
}
