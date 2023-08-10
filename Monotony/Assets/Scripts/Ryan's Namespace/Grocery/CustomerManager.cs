using System.Collections.Generic;
using UnityEngine;
using SebastiansNamespace;

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

        public Vector3 GetCheckoutPoint() => checkoutPoint.position;

        public Vector3 GetDeathPoint() => deathPoint.position;

        public void SpawnCustomer()
        {
            if (customers.Count <= 1) {
                Debug.Log("No more customers to spawn");
                GameObject.Find("FadeOut").GetComponent<FadeOut>().isFading = true;
            } else {
                int index = Random.Range(0, customers.Count);
                GameObject spawnedCustomer = (GameObject)Instantiate(customers[index], spawnPoint.position, Quaternion.identity);
                customers.RemoveAt(index);
            }
        }
    }
}
