using UnityEngine;

namespace RyansNamespace {
    public class BaggingArea : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other) {
            Debug.Log("called from baggingArea");
            if (other.CompareTag("Item")) {
                Item item = other.GetComponent<Item>();
                if (item.GetState() == Item.State.scanned)
                    item.SetState(Item.State.bagging);
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.CompareTag("Item")) {
                Item item = other.GetComponent<Item>();
                if (item.GetState() == Item.State.bagging)
                    item.SetState(Item.State.bagged);
            }
        }
    }
}
