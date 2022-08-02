using NerdStore.Core.DomainObjects.DTOs;

namespace NerdStore.Core.Messages.IntegrationEvents;

public class CanceledRequestEvent : IntegrationEvent
{
    public Guid RequestId { get; private set; }
    public Guid ClientId { get; private set; }
    public RequestItems RequestItems { get; private set; }

    public CanceledRequestEvent(Guid requestId, Guid clientId, RequestItems requestItems)
    {
        AggregateId = requestId;
        RequestId = requestId;
        ClientId = clientId;
        RequestItems = requestItems;
    }
}