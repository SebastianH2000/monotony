using UnityEngine;
using UnityEngine.UI;

namespace RyansNamespace {
    [RequireComponent(typeof(Slider))]
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        public void SetMaxHealth(float health) {
            slider.maxValue = health;
            slider.value = health;
        }

        public void SetHealth(float health) {
            slider.value = health;
        }
    }
}
