namespace OrderService.Application.Orders.GetOrdersByPersonId.Models;

public class GetOrdersByPersonIdRequest
{
    public required Guid PersonId { get; set; }
}
