using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class OrderRequest : MonoBehaviour
{
    [SerializeField] private Sprite[] foodSprites;
    private Dictionary<string, Sprite> foodSpriteDict = new Dictionary<string, Sprite>();

    private SpriteRenderer SR;

    private void Awake()
    {
        SR = GetComponent<SpriteRenderer>();

        foreach (Sprite sprite in foodSprites)
            foodSpriteDict.Add(sprite.name, sprite);
    }

    public void SetFood(Food food) {
        SR.sprite = foodSpriteDict[food.ToString()];
    }
    
    public void GoAway() {
        transform.parent.gameObject.SetActive(false);
    }
}
