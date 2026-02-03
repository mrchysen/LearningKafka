namespace OrderService.Domain;

public class Order
{
    public required string Name { get; set; }

    public required int Quantity { get; set; }
}
