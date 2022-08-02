namespace NerdStore.Core.Messages.IntegrationEvents;

public class RejectedRequestEvent : IntegrationEvent
{
    public Guid RequestId { get; private set; }
    public Guid ClientId { get; private set; }

    public RejectedRequestEvent(Guid requestId, Guid clientId)
    {
        AggregateId = requestId;
        RequestId = requestId;
        ClientId = clientId;
    }
}