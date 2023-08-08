using UnityEngine;

public class Customer : MonoBehaviour
{
    protected Food order;
    private bool orderTaken = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        order = (Food)Random.Range(0, 6);

        transform.GetChild(0).GetComponentInChildren<OrderRequest>().SetFood(order);
    }

    public bool TakeOrder(Food food) {
        if (orderTaken)
            return false;
        
        if (order == food)
            orderTaken = true;
        else
            orderTaken = false;

        return orderTaken;
    }
}
