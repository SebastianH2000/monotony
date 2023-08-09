using UnityEngine;

namespace RyansNamespace {
    [RequireComponent(typeof(SpriteRenderer))]
    public class Toothbrush : Brush
    {
        [Header("Toothbrush")]
        [SerializeField] private Sprite toothbrushWithPaste;

        private bool hasToothpaste = false;
        private SpriteRenderer SR;

        protected override void Awake()
        {
            base.Awake();
            SR = GetComponent<SpriteRenderer>();
        }

        protected override void OnMouseDown()
        {
            if (hasToothpaste)
                base.OnMouseDown();
        }

        public void AddToothpaste() {
            if (hasToothpaste)
                return;
            
            SR.sprite = toothbrushWithPaste;
            hasToothpaste = true;
        }
    }
}
