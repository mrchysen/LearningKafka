namespace OrderService.Application.Orders.CreateOrder.Models;

public class OrderResponse
{
    public decimal AllPrice { get; set; }

    public required string PersontId { get; set; }

    public List<OrderItemDto> OrderItems { get; set; } = [];

    public OrderStatus Status { get; set; }
}

public enum OrderStatus
{
    Paid,
    Pending,
    Failed
}