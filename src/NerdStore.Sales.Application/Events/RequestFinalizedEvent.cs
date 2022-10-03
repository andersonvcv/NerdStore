using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Events;

public class RequestFinalizedEvent : Event
{
    public Guid RequestId { get; private set; }

    public RequestFinalizedEvent(Guid requestId)
    {
        AggregateId = requestId;
        RequestId = requestId;
    }
}