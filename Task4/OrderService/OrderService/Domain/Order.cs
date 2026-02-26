namespace OrderService.Domain;

public class Order
{
    public required Guid Id { get; set; }

    public required List<OrderItem> OrderItems { get; set; }

    public required decimal TotalPrice { get; set; }

    public required Guid PersonId { get; set; }

    public DateTime CreatedAt { get; set; }

    public OrderStatus Status { get; set; }
}
