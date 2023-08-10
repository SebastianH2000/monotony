using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour
{
    public static Tray instance { get; private set;}

    [SerializeField] private GameObject[] foods;
    [SerializeField] private List<Transform> spawnPoints;
    private Dictionary<string, GameObject> foodDict = new Dictionary<string, GameObject>();
    [SerializeField] private Transform servePos;
    [SerializeField] private float speed;
    private Customer[] customers;
    private int customersServed = 0;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject food in foods)
            foodDict.Add(food.name, food);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnFood(Food food) {
        if (spawnPoints.Count <= 0)
            return;

        int randomIndex = Random.Range(0, spawnPoints.Count);
        GameObject instance = (GameObject)Instantiate(foodDict[food.ToString()], transform);
        instance.transform.localPosition = spawnPoints[randomIndex].localPosition;
        instance.transform.localRotation = Quaternion.identity;
        spawnPoints.RemoveAt(randomIndex);
    }

    public void ServedCustomer() {
        customersServed++;

        if (customersServed >= customers.Length) {
            GameObject.Find("FadeOut").GetComponent<FadeOut>().isFading = true;
        }
    }

    public void SetCustomers(Customer[] customers) {
        this.customers = customers;
    }

    public IEnumerator MoveToServePosition() {
        while (Vector2.Distance(transform.position, servePos.position) >= 0.01f) {
            transform.position = Vector2.MoveTowards(transform.position, servePos.position, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = servePos.position;
    }
}
