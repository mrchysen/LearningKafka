namespace OrderService.Application.Orders.GetActualOrdersByPersonId.Models;

public class GetOrdersByPersonIdDto
{
    public required Guid PersonId { get; set; }
}
