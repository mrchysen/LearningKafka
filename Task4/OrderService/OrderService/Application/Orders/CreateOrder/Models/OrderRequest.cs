using System.ComponentModel.DataAnnotations;

namespace OrderService.Application.Orders.CreateOrder.Models;

public class OrderRequest
{
    public required Guid PersonId { get; set; }

    [MinLength(1, ErrorMessage = "Order must contain at least one item")]
    public required List<OrderItemDto> OrderItems { get; set; } = [];
}
