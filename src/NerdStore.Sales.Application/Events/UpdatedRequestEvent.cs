using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Events;

public class UpdatedRequestEvent : Event
{
    public Guid ClientId { get; private set; }
    public Guid RequestId { get; private set; }
    public decimal Total { get; private set; }

    public UpdatedRequestEvent(Guid clientId, Guid requestId, decimal total)
    {
        AggregateId = requestId;
        ClientId = clientId;
        RequestId = requestId;
        Total = total;
    }
}