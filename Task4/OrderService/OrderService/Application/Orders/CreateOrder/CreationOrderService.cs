using Microsoft.EntityFrameworkCore;
using OrderService.Application.Orders.CreateOrder.Mappers;
using OrderService.Application.Orders.CreateOrder.Models;
using OrderService.Domain;
using OrderService.Infrastructure;

namespace OrderService.Application.Orders.CreateOrder;

public interface ICreationOrderService
{
    Task<OrderResponse> Create(CreateOrderParams orderParams, CancellationToken cancellationToken);
}

public class CreationOrderService(
    OrderServiceDbContext dbContext,
    ILogger<CreationOrderService> logger) : ICreationOrderService
{
    public async Task<OrderResponse> Create(CreateOrderParams orderParams, CancellationToken cancellationToken)
    {
        logger.LogInformation("Order created for person {personId}", orderParams.PersonId);

        var products = 
            await dbContext.Products
            .Where(x => orderParams.OrderItems.Select(oi => oi.Name).Contains(x.Name))
            .ToDictionaryAsync(x => x.Name, cancellationToken);

        CheckHasEnoughQuantity(products, orderParams.OrderItems);

        var orderId = Guid.NewGuid();

        dbContext.Add(new Order
        { 
            Id = orderId,
            PersonId = orderParams.PersonId,
            CreatedAt = DateTime.UtcNow,
            OrderItems = orderParams.OrderItems,
            TotalPrice = GetTotalPrice(products, orderParams.OrderItems)
        });

        // TODO: use outbox
        // Kafka

        DecreaseQuantity(products, orderParams.OrderItems);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new()
        {
            OrderId = orderId,
            Status = OrderStatus.Pending,
            AllPrice = 1000,
            OrderItems = orderParams.OrderItems.Select(CreateOrderMapper.MapToOrderItemDto).ToList(),
            PersontId = orderParams.PersonId
        };
    }

    private decimal GetTotalPrice(Dictionary<string, Product> products, List<OrderItem> orderItems)
    {
        decimal totalPrice = 0;

        foreach (var orderItem in orderItems)
        {
            if (products.TryGetValue(orderItem.Name, out var product))
            {
                totalPrice += product.Price * orderItem.Quantity;
            }
        }

        return totalPrice;
    }

    private void DecreaseQuantity(Dictionary<string, Product> products, List<OrderItem> orderItems)
    {
        foreach (var orderItem in orderItems)
        {
            if (products.TryGetValue(orderItem.Name, out var product))
            {
                product.Quantity -= orderItem.Quantity;
            }
        }
    }

    private void CheckHasEnoughQuantity(Dictionary<string, Product> products, List<OrderItem> orderItems)
    {
        if (products.Count != orderItems.Count)
        {
            throw new Exception("One or more products could not be found in the database");
        }

        var problems = new List<string>();

        foreach (var orderItem in orderItems)
        {
            var exist = products.TryGetValue(orderItem.Name, out var product);
            if (!exist)
                problems.Add($"{orderItem.Name} not found");
            else if (product!.Quantity < orderItem.Quantity)
                problems.Add($"{orderItem.Name} (available: {product.Quantity}, requested: {orderItem.Quantity})");
        }

        if(problems.Count > 0)
            throw new Exception($"Insufficient quantity: {string.Join("; ", problems)}");
    }
}
