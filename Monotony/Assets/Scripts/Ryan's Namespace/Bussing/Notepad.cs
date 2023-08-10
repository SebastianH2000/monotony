using System.Collections;
using UnityEngine;

public class Notepad : MonoBehaviour
{
    [SerializeField] private Transform outOfFramePos;
    [SerializeField] private float speed;
    private Customer[] customers;
    private int customersServed = 0;
    public bool allCustomersServed { get; private set; }

    // Start is called before the first frame update
    void Start() => customers = FindObjectsOfType<Customer>();

    public void HandleOrder(Food food, OrderButton orderButton) {
        foreach (Customer customer in customers) {
            if (customer.TakeOrder(food)) {
                Debug.Log("Order taken!");
                Tray.instance.SpawnFood(food);
                customersServed++;

                if (customersServed >= customers.Length) {
                    StartCoroutine(MoveOutOfFrame());
                    allCustomersServed = true;
                    orderButton.OnMouseExit();
                    return;
                }

                return;
            }
        }
        
        Debug.Log("Order not taken!");
    }

    private IEnumerator MoveOutOfFrame() {
        while (Vector2.Distance(transform.position, outOfFramePos.position) >= 0.01f) {
            transform.position = Vector2.MoveTowards(transform.position, outOfFramePos.position, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = outOfFramePos.position;

        Tray.instance.SetCustomers(customers);
        StartCoroutine(Tray.instance.MoveToServePosition());
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
