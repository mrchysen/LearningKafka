using OrderService.Domain;

namespace OrderService.Application.Orders.CreateOrder.Models;

public class OrderResponse
{
    public Guid OrderId { get; set; }

    public decimal AllPrice { get; set; }

    public required Guid PersontId { get; set; }

    public List<OrderItemDto> OrderItems { get; set; } = [];

    public OrderStatus Status { get; set; }
}
