using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RyansNamespace {
    public class Computer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI displayText;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void UpdateDisplayText(string text) {
            displayText.text = text;
        }

        public IEnumerator FlashingText(string text, int times, float delay) {
            for (int i = 0; i < times; i++) {
                displayText.text = text;
                yield return new WaitForSeconds(delay);
                displayText.text = "";
                yield return new WaitForSeconds(delay);
            }
        }
    }
}
