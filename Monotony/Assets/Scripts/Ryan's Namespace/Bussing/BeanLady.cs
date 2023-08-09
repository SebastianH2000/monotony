public class BeanLady : Customer
{
    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
        
        order = Food.Beans;
        orderRequest.SetFood(order);
    }
}
