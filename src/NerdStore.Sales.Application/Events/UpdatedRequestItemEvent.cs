using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Events;

public class UpdatedRequestItemEvent : Event
{
    public Guid ClientId { get; private set; }
    public Guid RequestId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }

    public UpdatedRequestItemEvent(Guid clientId, Guid requestId, Guid productId, int quantity)
    {
        AggregateId = requestId;
        ClientId = clientId;
        RequestId = requestId;
        ProductId = productId;
        Quantity = quantity;
    }
}