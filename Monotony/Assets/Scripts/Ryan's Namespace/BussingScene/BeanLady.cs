public class BeanLady : Customer
{
    // Start is called before the first frame update
    protected override void Start() {
        order = Food.Beans;

        transform.GetChild(0).GetComponentInChildren<OrderRequest>().SetFood(order);
    }
}
