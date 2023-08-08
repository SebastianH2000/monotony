using UnityEngine;

public class Notepad : MonoBehaviour
{
    private Customer[] customers;

    // Start is called before the first frame update
    void Start() => customers = FindObjectsOfType<Customer>();

    public void HandleOrder(Food food) {
        foreach (Customer customer in customers) {
            if (customer.TakeOrder(food)) {
                Debug.Log("Order taken!");
                return;
            }
        }
        
        Debug.Log("Order not taken!");
    }
}

public enum Food {
    Beans,
    Burger,
    Cake,
    ChickenTenders,
    Milkshake,
    Steak
}
