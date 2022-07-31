using MediatR;
using NerdStore.Core.Messages.IntegrationEvents;

namespace NerdStore.Sales.Application.Events;

public class RequestEventHandler : 
    INotificationHandler<DraftRequestEvent>, 
    INotificationHandler<UpdatedRequestEvent>, 
    INotificationHandler<AddedRequestItemEvent>,
    INotificationHandler<RejectedRequestEvent>
{
    public async Task Handle(DraftRequestEvent notification, CancellationToken cancellationToken)
    {
    }

    public async Task Handle(UpdatedRequestEvent notification, CancellationToken cancellationToken)
    {
    }

    public async Task Handle(AddedRequestItemEvent notification, CancellationToken cancellationToken)
    {
    }

    public async Task Handle(RejectedRequestEvent integrationEvent, CancellationToken cancellationToken)
    {
        // cancel request and return error
    }
}
