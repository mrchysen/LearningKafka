using OrderService.Domain;

namespace OrderService.Application.Orders.GetOrdersByPersonId.Models;

public class PersonOrdersResponse
{
    public required List<Order> Orders { get; set; }
}
