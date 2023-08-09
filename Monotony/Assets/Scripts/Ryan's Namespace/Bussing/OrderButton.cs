using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class OrderButton : MonoBehaviour
{
    [SerializeField] private Food food;
    private Notepad notepad;
    private SpriteRenderer SR;

    private void Awake()
    {
        notepad = GetComponentInParent<Notepad>();

        SR = GetComponent<SpriteRenderer>();
    }

    private void OnMouseEnter() {
        if (notepad.allCustomersServed)
            return;

        SR.color = Color.gray;
    }

    public void OnMouseExit() => SR.color = Color.white;

    private void OnMouseDown()
    {
        if (notepad.allCustomersServed)
            return;

        notepad.HandleOrder(food, this);
    }
}
