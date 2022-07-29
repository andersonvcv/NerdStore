using MediatR;

namespace NerdStore.Sales.Application.Events;

public class RequestEventHandler : 
    INotificationHandler<DraftRequestEvent>, 
    INotificationHandler<UpdatedRequestEvent>, 
    INotificationHandler<AddedRequestItemEvent>
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
}
