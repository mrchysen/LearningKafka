using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OrderService.Application.Orders.CreateOrder;
using OrderService.Application.Orders.GetActualOrdersByPersonId;
using OrderService.Application.Orders.GetOrdersByPersonId;
using OrderService.Domain;
using OrderService.Infrastructure;
using OrderService.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddSeq();
});
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.Configure<CommonSettings>(
    builder.Configuration);
builder.Services.Configure<KafkaSettings>(
    builder.Configuration.GetSection("ExternalServices:Kafka"));

// Application Services
builder.Services.AddScoped<ICreationOrderService, CreationOrderService>();
builder.Services.AddScoped<IGetOrdersByPersonIdService, GetOrdersByPersonIdService>();
builder.Services.AddScoped<IGetActualOrdersByPersonIdService, GetActualOrdersByPersonIdService>();

// Application Services Kafka
builder.Services.AddSingleton<IProducer<string, Order>>(sp =>
{
    var kafkaSettings = sp.GetRequiredService<IOptions<KafkaSettings>>().Value;
    var config = new ProducerConfig { BootstrapServers = kafkaSettings.BootstrapServers };

    return new ProducerBuilder<string, Order>(config)
        .SetValueSerializer(new KafkaJsonSerializer<Order>())
        .Build();
});

// Infrastructure Services
builder.Services.AddDbContext<OrderServiceDbContext>(options =>
    options.UseInMemoryDatabase("order_service_database"));

var app = builder.Build();

var s = app.Services.GetRequiredService<IOptions<CommonSettings>>();
var s2 = app.Services.GetRequiredService<IOptions<KafkaSettings>>();

app.UseMiddleware<LoggingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<OrderServiceDbContext>();
    await DbInitializer.SeedAsync(db);
}

app.Run();
