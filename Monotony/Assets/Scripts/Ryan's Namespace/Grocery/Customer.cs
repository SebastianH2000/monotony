using System.Collections.Generic;
using UnityEngine;

namespace RyansNamespace {
    public class Customer : MonoBehaviour
    {
        [SerializeField] private List<GameObject> items;
        [SerializeField] private float speed;

        private Vector3 checkoutPoint;
        private Vector3 itemSpawnPoint;
        private Vector3 deathPoint;

        private Vector3 target;

        private bool arrived = false;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        public void SetUp(Vector3 destination, Vector3 itemSpawnPoint, Vector3 deathPoint)
        {
            this.checkoutPoint = destination;
            this.itemSpawnPoint = itemSpawnPoint;
            this.deathPoint = deathPoint;

            target = checkoutPoint;
        }

        // Update is called once per frame
        void Update()
        {
            if (arrived)
                return;

            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target) < 0.1f) {
                arrived = true;

                if (target == checkoutPoint) {
                    SpawnItem();
                } else if (target == deathPoint) {
                    CustomerManager.instance.SpawnCustomer();
                    Destroy(gameObject);
                }
            }
        }

        public void SpawnItem() {
            if (items.Count <= 0) {
                target = deathPoint;
                arrived = false;
                return;
            }

            int index = Random.Range(0, items.Count);
            GameObject spawnedItem = (GameObject)Instantiate(items[index], itemSpawnPoint, Quaternion.identity);
            spawnedItem.GetComponent<Item>().SetUp(this);
            items.RemoveAt(index);
        }
    }
}
