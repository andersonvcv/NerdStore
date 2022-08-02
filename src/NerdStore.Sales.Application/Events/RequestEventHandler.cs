using MediatR;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.IntegrationEvents;
using NerdStore.Sales.Application.Commands;

namespace NerdStore.Sales.Application.Events;

public class RequestEventHandler : 
    INotificationHandler<DraftRequestEvent>, 
    INotificationHandler<UpdatedRequestEvent>, 
    INotificationHandler<AddedRequestItemEvent>,
    INotificationHandler<RejectedRequestEvent>,
    INotificationHandler<CompletedPaymentEvent>,
    INotificationHandler<PaymentRejectedEvent>
{
    private readonly IMediatoRHandler _mediatoRHandler;

    public RequestEventHandler(IMediatoRHandler mediatoRHandler)
    {
        _mediatoRHandler = mediatoRHandler;
    }

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
        await _mediatoRHandler.SendCommand(new CancelRequestCommamnd(integrationEvent.RequestId,
            integrationEvent.ClientId));
    }

    public async Task Handle(CompletedPaymentEvent integrationEvent, CancellationToken cancellationToken)
    {
        await _mediatoRHandler.SendCommand(new FinalizeRequestCommand(integrationEvent.RequestId,
            integrationEvent.ClientId));
    }

    public async Task Handle(PaymentRejectedEvent integrationEvent, CancellationToken cancellationToken)
    {
        await _mediatoRHandler.SendCommand(new CancelRequestReplenishStockCommand(integrationEvent.RequestId,
            integrationEvent.ClientId));
    }
}
