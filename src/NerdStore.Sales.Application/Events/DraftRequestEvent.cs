using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Events;

public class DraftRequestEvent : Event
{
    public Guid ClientId { get; private set; }
    public Guid RequestId { get; private set; }

    public DraftRequestEvent(Guid clientId, Guid requestId)
    {
        AggregateId = requestId;
        ClientId = clientId;
        RequestId = requestId;
    }
}