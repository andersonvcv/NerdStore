using MediatR;

namespace NerdStore.Catalog.Domain.Events
{
    public class LowStockEventHandler : INotificationHandler<LowStockEvent>
    {
        private readonly IProductRepository _productRepository;

        public LowStockEventHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Handle(LowStockEvent notification, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(notification.AggregateId);

            // TODO: Do something to replenish stock
        }
    }
}
