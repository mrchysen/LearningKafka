namespace OrderService.Domain;

public class OrderItem
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required int Quantity { get; set; }
}
