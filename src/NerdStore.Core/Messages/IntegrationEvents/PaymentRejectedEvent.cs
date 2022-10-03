namespace NerdStore.Core.Messages.IntegrationEvents;

public class PaymentRejectedEvent : IntegrationEvent
{
    public Guid RequestId { get; private set; }
    public Guid ClientId { get; private set; }
    public Guid PaymentId { get; private set; }
    public Guid TransactionId { get; private set; }
    public decimal Total { get; private set; }

    public PaymentRejectedEvent(Guid requestId, Guid clientId, Guid paymentId, Guid transactionId, decimal total)
    {
        AggregateId = requestId;
        RequestId = requestId;
        ClientId = clientId;
        PaymentId = paymentId;
        TransactionId = transactionId;
        Total = total;
    }
}