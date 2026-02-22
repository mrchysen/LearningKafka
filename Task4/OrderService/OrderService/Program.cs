using Microsoft.EntityFrameworkCore;
using OrderService.Application.Orders.CreateOrder;
using OrderService.Application.Orders.GetActualOrdersByPersonId;
using OrderService.Application.Orders.GetOrdersByPersonId;
using OrderService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddSeq();
});
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Application Services
builder.Services.AddScoped<ICreationOrderService, CreationOrderService>();
builder.Services.AddScoped<IGetOrdersByPersonIdService, GetOrdersByPersonIdService>();
builder.Services.AddScoped<IGetActualOrdersByPersonIdService, GetActualOrdersByPersonIdService>();

// Infrastructure Services
builder.Services.AddDbContext<OrderServiceDbContext>(options =>
    options.UseInMemoryDatabase("order_service_database"));

var app = builder.Build();

app.UseMiddleware<LoggingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
