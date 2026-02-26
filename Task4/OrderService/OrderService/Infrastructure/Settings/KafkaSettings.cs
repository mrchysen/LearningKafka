namespace OrderService.Infrastructure.Settings;

public class KafkaSettings
{
    public string BootstrapServers { get; set; } = null!;

    public string OrderTopic { get; set; } = null!;

    public string PaymentTopic { get; set; } = null!;

    public string GroupId { get; set; } = null!;
}