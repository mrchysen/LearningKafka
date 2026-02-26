namespace OrderService.Domain;

public class PaymentResult
{
    public Guid OrderId { get; set; }

    public OrderStatus Status { get; set; }
}
