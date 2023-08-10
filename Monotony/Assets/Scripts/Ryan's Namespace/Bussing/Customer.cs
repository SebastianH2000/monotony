using UnityEngine;

public class Customer : MonoBehaviour
{
    protected Food order;
    protected OrderRequest orderRequest;
    private bool orderDelivered = false;
    private bool orderTaken = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        order = (Food)Random.Range(0, 6);

        orderRequest = transform.GetChild(0).GetComponentInChildren<OrderRequest>();
        orderRequest.SetFood(order);
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

    public bool TakeFood(Food food) {
        if (orderDelivered)
            return false;
        
        if (order == food) {
            orderDelivered = true;
            orderRequest.GoAway();
        } else {
            orderDelivered = false;
        }

        return orderDelivered;
    }
}
