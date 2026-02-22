using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Orders.CreateOrder;
using OrderService.Application.Orders.CreateOrder.Mappers;
using OrderService.Application.Orders.CreateOrder.Models;
using OrderService.Application.Orders.GetActualOrdersByPersonId;
using OrderService.Application.Orders.GetActualOrdersByPersonId.Models;
using OrderService.Application.Orders.GetOrdersByPersonId;
using OrderService.Application.Orders.GetOrdersByPersonId.Models;

namespace OrderService.Controllers.Orders;

[ApiController]
[Route("orders")]
public class OrderController(
    ICreationOrderService creationOrderService,
    IGetOrdersByPersonIdService getOrdersByPersonIdService,
    IGetActualOrdersByPersonIdService getActualOrdersByPersonIdService,
    ILogger<OrderController> logger) : ControllerBase
{
    [HttpPost]
    public async Task<OrderResponse> CreateOrder(OrderRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Create order request from {personId}", request.PersonId);

        return await creationOrderService.Create(
            CreateOrderMapper.MapToCreateOrderParams(request),
            cancellationToken);
    }

    [HttpGet("{personId}")]
    public async Task<PersonOrdersResponse> GetOrdersByPerson(Guid personId, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get orders request for person with id: {personId}", personId);

        return await getOrdersByPersonIdService.GetOrders(
            new() { PersonId = personId },
            cancellationToken);
    }

    [HttpGet("{personId}/actual")]
    public async Task<ActualPersonOrdersResponse> GetActualOrdersByPerson(Guid personId, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get orders request for person with id: {personId}", personId);

        return await getActualOrdersByPersonIdService.GetOrders(
            new() { PersonId = personId },
            cancellationToken);
    }
}
