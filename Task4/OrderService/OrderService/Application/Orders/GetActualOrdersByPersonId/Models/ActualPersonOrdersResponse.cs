using OrderService.Domain;

namespace OrderService.Application.Orders.GetActualOrdersByPersonId.Models;

public class ActualPersonOrdersResponse
{
    public required List<Order> Orders { get; set; }
}
