using Microsoft.EntityFrameworkCore;
using OrderService.Domain;
using OrderService.Infrastructure;

namespace OrderService.Application.Orders.ChangeOrderStatus;

public interface IChangeOrderStatusService
{
    Task ChangeStatus(Guid orderId, OrderStatus newStatus, CancellationToken cancellationToken);
}

public class ChangeOrderStatusService(
    OrderServiceDbContext dbContext,
    ILogger<ChangeOrderStatusService> logger) : IChangeOrderStatusService
{
    public async Task ChangeStatus(Guid orderId, OrderStatus newStatus, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders.SingleOrDefaultAsync(or => or.Id == orderId, cancellationToken);

        if (order == null)
        {
            logger.LogWarning("Order with id {OrderId} do not exist. Status {NewStatus} received.", orderId, newStatus);
        }

        order!.Status = newStatus;

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
