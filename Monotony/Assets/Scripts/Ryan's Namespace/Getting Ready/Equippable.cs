using UnityEngine;

namespace RyansNamespace {
    public class Equippable : Drag
    {
        [Header("Collision Detection")]
        [SerializeField] private float timeBetweenChecks;
        [SerializeField] private float radius;
        [SerializeField] private LayerMask selectedLayer;
        private float timer;

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            if (timer > 0) {
                timer -= Time.fixedDeltaTime;
            } else {
                timer = timeBetweenChecks;
                
                Collider2D collider = Physics2D.OverlapCircle(RB.position, radius, selectedLayer);

                if (collider != null)
                    Equip(collider.gameObject);
            }
        }

        protected virtual void Equip(GameObject GO) {

        }
    }
}
