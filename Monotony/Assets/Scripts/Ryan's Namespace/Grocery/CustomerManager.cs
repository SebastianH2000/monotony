using System.Collections.Generic;
using UnityEngine;

namespace RyansNamespace {
    public class CustomerManager : MonoBehaviour
    {
        public static CustomerManager instance { get; private set; }

        [SerializeField] private List<GameObject> customers;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform checkoutPoint;
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

        public Vector3 GetCheckoutPoint() {
            return checkoutPoint.position;
        }

        public Vector3 GetDeathPoint() {
            return deathPoint.position;
        }

        public void SpawnCustomer()
        {
            if (customers.Count <= 0) {
                Debug.Log("No more customers to spawn");
            } else {
                int index = Random.Range(0, customers.Count);
                GameObject spawnedCustomer = (GameObject)Instantiate(customers[index], spawnPoint.position, Quaternion.identity);
                customers.RemoveAt(index);
            }
        }
    }
}
