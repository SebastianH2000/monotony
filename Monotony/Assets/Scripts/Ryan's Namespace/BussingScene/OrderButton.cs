using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class OrderButton : MonoBehaviour
{
    [SerializeField] private Food food;
    private SpriteRenderer SR;

    private void Awake() => SR = GetComponent<SpriteRenderer>();

    private void OnMouseEnter() => SR.color = Color.gray;

    private void OnMouseExit() => SR.color = Color.white;

    private void OnMouseDown() => GetComponentInParent<Notepad>().HandleOrder(food);
}
