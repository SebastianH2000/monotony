using System.Collections.Generic;
using UnityEngine;

namespace RyansNamespace {
    public class CustomerManager : MonoBehaviour
    {
        public static CustomerManager instance { get; private set; }

        [SerializeField] private List<GameObject> customers;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform checkoutPoint;
        [SerializeField] private Transform itemSpawnPoint;
        [SerializeField] private Transform deathPoint;

        // Start is called before the first frame update
        void Start()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            SpawnCustomer();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void SpawnCustomer()
        {
            if (customers.Count <= 0) {
                Debug.Log("No more customers to spawn");
                return;
            }

            int index = Random.Range(0, customers.Count);
            GameObject spawnedCustomemr = (GameObject)Instantiate(customers[index], spawnPoint.position, Quaternion.identity);
            spawnedCustomemr.GetComponent<Customer>().SetUp(checkoutPoint.position, itemSpawnPoint.position, deathPoint.position);
            customers.RemoveAt(index);
        }
    }
}
