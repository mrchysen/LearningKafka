using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Orders.CreateOrder;
using OrderService.Application.Orders.CreateOrder.Mappers;
using OrderService.Application.Orders.CreateOrder.Models;

namespace OrderService.Controllers.Orders;

[ApiController]
[Route("orders")]
public class OrderController(
    ICreationOrderService creationOrderService,
    ILogger<OrderController> logger) : ControllerBase
{
    [HttpPost]
    public async Task<OrderResponse> CreateOrder(OrderRequest request)
    {
        logger.LogInformation("Get order request from {personId}", request.PersonId);

        return await creationOrderService.Create(
            CreateOrderMapper.MapToCreateOrderParams(
                request));
    }
}
