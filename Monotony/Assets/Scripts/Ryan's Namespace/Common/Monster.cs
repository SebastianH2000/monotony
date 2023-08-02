using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RyansNamespace {
    [RequireComponent(typeof(Rigidbody2D))]
    public class Monster : MonoBehaviour
    {
        public enum Type {
            Static,
            Wanderer,
            Chaser
        }

        [SerializeField] private float speed;
        [SerializeField] private float minTimeBetweenDirectionChange;
        [SerializeField] private float maxTimeBetweenDirectionChange;
        [SerializeField] private Type type;
        private float timer;
        private Vector2 direction;
        private Rigidbody2D RB;
        private Vector2 target;

        public void SetTarget(Vector2 target) {
            this.target = target;
        }

        public void SetType(Type type) {
            this.type = type;
        }

        private void Awake() {
            RB = GetComponent<Rigidbody2D>();
        }

        // Start is called before the first frame update
        void Start()
        {
            RB.bodyType = RigidbodyType2D.Kinematic;
        }

        // Update is called once per frame
        void Update()
        {
            switch (type) {
                case Type.Static:
                    break;
                case Type.Wanderer:
                    if (timer <= 0) {
                        direction = Random.insideUnitCircle.normalized;
                        timer = Random.Range(minTimeBetweenDirectionChange, maxTimeBetweenDirectionChange);
                    } else {
                        timer -= Time.deltaTime;
                    }
                    break;
                case Type.Chaser:
                    break;
            }
        }

        private void FixedUpdate() {
            switch (type) {
                case Type.Static:
                    break;
                case Type.Wanderer:
                    RB.MovePosition(RB.position + direction * speed * Time.fixedDeltaTime);
                    break;
                case Type.Chaser:
                    RB.MovePosition(Vector2.MoveTowards(RB.position, target, speed * Time.fixedDeltaTime));

                    if (Vector2.Distance(RB.position, target) < 0.1f)
                        type = Type.Static;
                    break;
            }
        }
    }
}
