using Microsoft.EntityFrameworkCore;
using OrderService.Application.Orders.CreateOrder.Mappers;
using OrderService.Application.Orders.CreateOrder.Models;
using OrderService.Infrastructure;

namespace OrderService.Application.Orders.CreateOrder;

public interface ICreationOrderService
{
    Task<OrderResponse> Create(CreateOrderParams orderParams);
}

public class CreationOrderService(
    OrderServiceDbContext dbContext,
    ILogger<CreationOrderService> logger) : ICreationOrderService
{
    public async Task<OrderResponse> Create(CreateOrderParams orderParams)
    {
        logger.LogInformation("Order created for person {personId}", orderParams.PersonId);

        // Check this product in database
        var isAllProductsExists = 
            dbContext.OrderItems
            .Where(x => orderParams.OrderItems.Select(oi => oi.Name).Contains(x.Name))
            .Count() == orderParams.OrderItems.Count;


        // In transaction
        // Create order in database
        // Create outbox event about order in database
        // Add job that read from outbox



        return new()
        {
            Status = OrderStatus.Pending,
            AllPrice = 1000,
            OrderItems = orderParams.OrderItems.Select(CreateOrderMapper.MapToOrderItemDto).ToList(),
            PersontId = orderParams.PersonId
        };
    }
}
