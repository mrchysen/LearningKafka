using OrderService.Domain;

namespace OrderService.Application.Orders.CreateOrder.Models;

public class CreateOrderParams
{
    public required string PersonId { get; set; }

    public List<OrderItem> OrderItems { get; set; } = [];
}
