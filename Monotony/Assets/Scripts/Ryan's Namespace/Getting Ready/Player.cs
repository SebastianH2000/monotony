using System.Collections.Generic;
using UnityEngine;

namespace RyansNamespace {
    [RequireComponent(typeof(SpriteRenderer))]
    public class Player : MonoBehaviour
    {
        public static Player instance { get; private set; }

        [SerializeField] private GameObject toothbrushCheckmark;
        [SerializeField] private GameObject hairbrushCheckmark;
        [SerializeField] private GameObject skincareCheckmark;

        public enum State
        {
            NothingDone = 0,
            HairDone = 1,
            SkincareDone = 2,
            BrushingTeeth = 4,
            // Add more tasks as needed, making sure each value is a power of 2 (1, 2, 4, 8, 16, ...)
        }

        [SerializeField] private Sprite[] characterSprites;

        private Dictionary<string, Sprite> characterSpriteDictionary = new Dictionary<string, Sprite>();
        private State currentState = State.NothingDone;
        private SpriteRenderer SR;

        // Start is called before the first frame update
        void Start() {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            SR = GetComponent<SpriteRenderer>();

            // Populate the dictionary with the character sprites
            foreach (Sprite sprite in characterSprites)
            {
                characterSpriteDictionary.Add(sprite.name, sprite);
            }
        }

        public void CompleteHairTask()
        {
            currentState |= State.HairDone;
            hairbrushCheckmark.SetActive(true);
            UpdateCharacterSprite();
        }

        public void CompleteSkincareTask()
        {
            currentState |= State.SkincareDone;
            skincareCheckmark.SetActive(true);
            UpdateCharacterSprite();
        }

        public void StartBrushingTeeth()
        {
            currentState |= State.BrushingTeeth;
            UpdateCharacterSprite();
        }

        public void StopBrushingTeeth()
        {
            currentState &= ~State.BrushingTeeth;
            toothbrushCheckmark.SetActive(true);
            UpdateCharacterSprite();
        }

        private Sprite GetCharacterSprite()
        {
            // Define your sprite mappings based on the character's tasks
            // Here's an example using a switch statement:

            switch (currentState)
            {
                case State.HairDone | State.SkincareDone | State.BrushingTeeth:
                    return characterSpriteDictionary["Hair Done Skincare Done Brushing Teeth"];
                case State.HairDone | State.SkincareDone:
                    return characterSpriteDictionary["Hair Done Skincare Done"];
                case State.HairDone | State.BrushingTeeth:
                    return characterSpriteDictionary["Hair Done Brushing Teeth"];
                case State.SkincareDone | State.BrushingTeeth:
                    return characterSpriteDictionary["Skincare Done Brushing Teeth"];
                case State.HairDone:
                    return characterSpriteDictionary["Hair Done"];
                case State.SkincareDone:
                    return characterSpriteDictionary["Skincare Done"];
                case State.BrushingTeeth:
                    return characterSpriteDictionary["Brushing Teeth"];
                default:
                    return characterSpriteDictionary["Nothing Done"];
            }
        }

        private void UpdateCharacterSprite() => SR.sprite = GetCharacterSprite();
    }
}
