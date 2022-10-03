using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Commands;

public class FinalizeRequestCommand : Command
{
    public Guid RequestId { get; private set; }
    public Guid ClientId { get; private set; }

    public FinalizeRequestCommand(Guid requestId, Guid clientId)
    {
        AggregateId = requestId;
        RequestId = requestId;
        ClientId = clientId;
    }
}