using UnityEngine;
using RyansNamespace;

[RequireComponent(typeof(Collider2D))]
public class DeliveredFood : Drag
{
    [SerializeField] private Food food;
    [SerializeField] private float timeBetweenChecks;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask customerLayer;
    private bool delivered = false;
    private float timer;

    protected override void FixedUpdate() {
        base.FixedUpdate();

        if (timer > 0f) {
            timer -= Time.fixedDeltaTime;
        } else {
            timer = timeBetweenChecks;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(RB.position, checkRadius, customerLayer);

            foreach (Collider2D collider in colliders) {
                Customer customer = collider.GetComponent<Customer>();
                if (customer.TakeFood(food)) {
                    Debug.Log("food taken");
                    AppManager.instance.sfxManager.PlaySFX("fulfill_order_plate");
                    Tray.instance.ServedCustomer();
                    delivered = true;
                    OnMouseUp();
                    Destroy(gameObject, 0.1f);
                } else {
                    Debug.Log("Wrong person!");
                    AppManager.instance.sfxManager.PlaySFX("hmm_" + Random.Range(1, 5).ToString());
                }
            }
        }
    }

    protected override void OnMouseDown()
    {
        if (delivered)
            return;

        transform.SetParent(null);
        base.OnMouseDown();
    }
}
