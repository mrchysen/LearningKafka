using Confluent.Kafka;
using Microsoft.Extensions.Options;
using OrderService.Application.Orders.ChangeOrderStatus;
using OrderService.Domain;
using OrderService.Infrastructure.Settings;

namespace OrderService.Infrastructure;

// Payment service can send Paid status or failed
public class PaymentConsumer(
    IOptions<KafkaSettings> settings,
    IConsumer<string, PaymentResult> consumer,
    ServiceProvider serviceProvider,
    ILogger<PaymentConsumer> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        consumer.Subscribe(settings.Value.PaymentTopic);

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = consumer.Consume(stoppingToken);

                    logger.LogInformation(
                        "Received message: {Message} from topic: {Topic}",
                        consumeResult.Message.Value,
                        consumeResult.Topic);

                    using(var scope = serviceProvider.CreateScope())
                    {
                        var changeOrderStatusService = scope.ServiceProvider.GetRequiredService<IChangeOrderStatusService>();

                        await changeOrderStatusService.ChangeStatus(
                        consumeResult.Message.Value.OrderId,
                        consumeResult.Message.Value.Status,
                        stoppingToken);
                    }

                    consumer.Commit(consumeResult);
                }
                catch (ConsumeException ex)
                {
                    logger.LogError(ex, "Error consuming message");
                }
            }
        }
        finally
        {
            consumer.Close();
        }
    }
}
