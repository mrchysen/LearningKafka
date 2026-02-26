using Microsoft.EntityFrameworkCore;
using OrderService.Application.Orders.GetActualOrdersByPersonId.Models;
using OrderService.Application.Orders.GetOrdersByPersonId.Models;
using OrderService.Domain;
using OrderService.Infrastructure;

namespace OrderService.Application.Orders.GetActualOrdersByPersonId;

public interface IGetActualOrdersByPersonIdService
{
    Task<ActualPersonOrdersResponse> GetOrders(GetOrdersByPersonIdRequest request, CancellationToken cancellationToken);
}

public class GetActualOrdersByPersonIdService(
    OrderServiceDbContext dbContext,
    ILogger<GetActualOrdersByPersonIdService> logger) : IGetActualOrdersByPersonIdService
{
    public async Task<ActualPersonOrdersResponse> GetOrders(GetOrdersByPersonIdRequest request, CancellationToken cancellationToken)
    {
        if (!IsUserCanReadSuchOrders(request))
        {
            logger.LogWarning("User does not have permission to access orders for this person with id {personId}", request.PersonId);
            throw new Exception("User does not have permission to access orders for this person");
        }

        var orders = await dbContext.Orders
            .Where(or => or.PersonId == request.PersonId)
            .Where(or => or.Status == OrderStatus.Pending || or.Status == OrderStatus.Paid)
            .ToListAsync(cancellationToken);

        return new ActualPersonOrdersResponse { Orders = orders };
    }

    // TODO: create IOrdersAccessVerifier service to validate
    private bool IsUserCanReadSuchOrders(GetOrdersByPersonIdRequest request)
    {
        return true;
    }
}
