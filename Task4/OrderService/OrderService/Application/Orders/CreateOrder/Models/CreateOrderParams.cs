using OrderService.Domain;

namespace OrderService.Application.Orders.CreateOrder.Models;

public class CreateOrderParams
{
    public required Guid PersonId { get; set; }

    public List<OrderItem> OrderItems { get; set; } = [];
}
