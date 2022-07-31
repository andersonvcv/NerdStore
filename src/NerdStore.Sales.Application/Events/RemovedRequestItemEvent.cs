using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Events;

public class RemovedRequestItemEvent : Event
{
    public Guid ClientId { get; private set; }
    public Guid RequestId { get; private set; }
    public Guid ProductId { get; private set; }

    public RemovedRequestItemEvent(Guid clientId, Guid requestId, Guid productId)
    {
        AggregateId = requestId;
        ClientId = clientId;
        RequestId = requestId;
        ProductId = productId;
    }
}