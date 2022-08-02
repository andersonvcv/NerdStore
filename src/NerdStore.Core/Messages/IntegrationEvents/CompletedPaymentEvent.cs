namespace NerdStore.Core.Messages.IntegrationEvents;

public class CompletedPaymentEvent : IntegrationEvent
{
    public Guid RequestId { get; private set; }
    public Guid ClientId { get; private set; }
    public Guid PaymentId { get; private set; }
    public Guid TransactionId { get; private set; }
    public decimal Total { get; private set; }

    public CompletedPaymentEvent(Guid requestId, Guid clientId, Guid paymentId, Guid transactionId, decimal total)
    {
        RequestId = requestId;
        ClientId = clientId;
        PaymentId = paymentId;
        TransactionId = transactionId;
        Total = total;
    }
}