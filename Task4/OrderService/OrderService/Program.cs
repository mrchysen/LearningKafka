using OrderService;
using OrderService.Application.Orders.CreateOrder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddSeq();
});
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Application Services
builder.Services.AddScoped<ICreationOrderService, CreationOrderService>();

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
