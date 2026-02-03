using OrderService.Application.Orders.CreateOrder.Models;
using OrderService.Domain;

namespace OrderService.Application.Orders.CreateOrder.Mappers;

public static class CreateOrderMapper
{
    public static CreateOrderParams MapToCreateOrderParams(OrderRequest orderRequest)
    {
        ArgumentNullException.ThrowIfNull(orderRequest);

        return new CreateOrderParams
        {
            PersonId = orderRequest.PersonId,
            OrderItems = orderRequest.OrderItems?.Select(MapToOrderItem).ToList() ?? []
        };
    }

    public static OrderItemDto MapToOrderItemDto(OrderItem orderItem)
    {
        ArgumentNullException.ThrowIfNull(orderItem);

        return new OrderItemDto
        {
            Name = orderItem.Name,
            Quantity = orderItem.Quantity
        };
    }

    private static OrderItem MapToOrderItem(OrderItemDto orderItem)
    {
        ArgumentNullException.ThrowIfNull(orderItem);

        return new OrderItem
        {
            Name = orderItem.Name,
            Quantity = orderItem.Quantity
        };
    }
}
