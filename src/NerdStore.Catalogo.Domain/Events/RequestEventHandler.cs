using MediatR;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.IntegrationEvents;

namespace NerdStore.Catalog.Domain.Events
{
    public class RequestEventHandler : 
        INotificationHandler<LowStockEvent>,
        INotificationHandler<InitiatedRequestEvent>,
        INotificationHandler<CanceledRequestEvent>

    {
        private readonly IProductRepository _productRepository;
        private readonly IStockService _stockService;
        private readonly IMediatoRHandler _mediatoRHandler;

        public RequestEventHandler(IProductRepository productRepository, IStockService stockService, IMediatoRHandler mediatoRHandler)
        {
            _productRepository = productRepository;
            _stockService = stockService;
            _mediatoRHandler = mediatoRHandler;
        }

        public async Task Handle(LowStockEvent notification, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(notification.AggregateId);

            // TODO: Do something to replenish stock
        }

        public async Task Handle(InitiatedRequestEvent integrationEvent, CancellationToken cancellationToken)
        {
            var hasRemovedFromStock = await _stockService.RemoveFromStock(integrationEvent.RequestItems);

            if (hasRemovedFromStock)
            {
                await _mediatoRHandler.PublishEvent(new ConfirmedRequestEvent(integrationEvent.RequestId, integrationEvent.ClientId, integrationEvent.RequestItems, integrationEvent.Total, integrationEvent.CardName, integrationEvent.CardNumber, integrationEvent.CardExpirationDate, integrationEvent.CardCVV));
            }
            else
            {
                await _mediatoRHandler.PublishEvent(new RejectedRequestEvent(integrationEvent.RequestId,
                    integrationEvent.ClientId));
            }
        }

        public async Task Handle(CanceledRequestEvent integrationEvent, CancellationToken cancellationToken)
        {
            await _stockService.AddToStock(integrationEvent.RequestItems);
        }
    }
}
