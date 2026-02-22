using Microsoft.EntityFrameworkCore;
using OrderService.Application.Orders.GetActualOrdersByPersonId;
using OrderService.Application.Orders.GetOrdersByPersonId.Models;
using OrderService.Infrastructure;

namespace OrderService.Application.Orders.GetOrdersByPersonId;

public interface IGetOrdersByPersonIdService
{
    Task<PersonOrdersResponse> GetOrders(GetOrdersByPersonIdRequest request, CancellationToken cancellationToken);
}

public class GetOrdersByPersonIdService(
    OrderServiceDbContext dbContext,
    ILogger<GetActualOrdersByPersonIdService> logger) : IGetOrdersByPersonIdService
{
    public async Task<PersonOrdersResponse> GetOrders(GetOrdersByPersonIdRequest request, CancellationToken cancellationToken)
    {
        if (!IsUserCanReadSuchOrders(request))
        {
            logger.LogWarning("User does not have permission to access orders for this person with id {personId}", request.PersonId);
            throw new Exception("User does not have permission to access orders for this person");
        }

        var orders = await dbContext.Orders
            .Where(or => or.PersonId == request.PersonId)
            .ToListAsync(cancellationToken);

        return new PersonOrdersResponse { Orders = orders };
    }

    // TODO: create IOrdersAccessVerifier service to validate
    private bool IsUserCanReadSuchOrders(GetOrdersByPersonIdRequest request)
    {
        return true;
    }
}
