using System.Collections.Generic;
using UnityEngine;

namespace RyansNamespace {
    public class Customer : MonoBehaviour
    {
        [SerializeField] private List<GameObject> items;
        [SerializeField] private float speed;

        private Vector3 target;
        private bool arrived = false;

        // Start is called before the first frame update
        void Start() => target = CustomerManager.instance.GetCheckoutPoint();

        // Update is called once per frame
        void Update()
        {
            if (arrived)
                return;

            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target) < 0.1f) {
                arrived = true;

                if (target == CustomerManager.instance.GetCheckoutPoint()) {
                    SpawnItem();
                } else if (target == CustomerManager.instance.GetDeathPoint()) {
                    CustomerManager.instance.SpawnCustomer();
                    Destroy(gameObject);
                }
            }
        }

        public void SpawnItem() {
            if (items.Count <= 0) {
                target = CustomerManager.instance.GetDeathPoint();
                arrived = false;
            } else {
                int index = Random.Range(0, items.Count);
                GameObject spawnedItem = (GameObject)Instantiate(items[index], transform.position, Quaternion.identity);
                spawnedItem.GetComponent<Item>().SetUp(this);
                items.RemoveAt(index);
            }
        }
    }
}
