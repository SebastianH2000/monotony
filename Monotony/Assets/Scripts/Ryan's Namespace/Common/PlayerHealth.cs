using UnityEngine;

namespace RyansNamespace {
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private HealthBar healthBar;
        [SerializeField] private float maxHealth;
        [SerializeField] private float damagePerSecond;
        private float currentHealth;

        // Start is called before the first frame update
        void Start()
        {
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(currentHealth);
        }

        public void TakeDamage() {
            currentHealth = Mathf.Clamp(currentHealth - damagePerSecond * Time.deltaTime, 0f, maxHealth);
            healthBar.SetHealth(currentHealth);
        }
    }
}
