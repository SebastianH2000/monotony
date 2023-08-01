using UnityEngine;

namespace RyansNamespace {
    public class Player : MonoBehaviour
    {
        [SerializeField] private HealthBar healthBar;
        [SerializeField] private float maxHealth;
        private float currentHealth;

        // Start is called before the first frame update
        void Start()
        {
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(currentHealth);
        }

        public void TakeDamage(float damage) {
            currentHealth = Mathf.Clamp(currentHealth - damage, 0f, maxHealth);
            healthBar.SetHealth(currentHealth);
        }
    }
}
