using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain.Events
{
    public class LowStockEvent : DomainEvent
    {
        public int QuantityAvailableInStock { get; private set; }

        public LowStockEvent(Guid aggregateId, int quantityAvailableInStock) : base(aggregateId)
        {
            QuantityAvailableInStock = quantityAvailableInStock;
        }
    }
}
