using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RyansNamespace {
    [RequireComponent(typeof(Rigidbody2D))]
    public class Monster : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float minTimeBetweenDirectionChange;
        [SerializeField] private float maxTimeBetweenDirectionChange;
        private float timer;
        private Vector2 direction;
        private Rigidbody2D RB;

        private void Awake() {
            RB = GetComponent<Rigidbody2D>();
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (timer <= 0) {
                direction = Random.insideUnitCircle.normalized;
                timer = Random.Range(minTimeBetweenDirectionChange, maxTimeBetweenDirectionChange);
            } else {
                timer -= Time.deltaTime;
            }
        }

        private void FixedUpdate() {
            RB.MovePosition(RB.position + direction * speed * Time.fixedDeltaTime);
        }
    }
}
