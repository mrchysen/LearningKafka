using System.ComponentModel.DataAnnotations;

namespace OrderService.Application.Orders.CreateOrder.Models;

public class OrderItemDto
{
    [MinLength(1)]
    public required string Name { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public required int Quantity { get; set; }
}