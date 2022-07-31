using NerdStore.Core.DomainObjects.DTOs;

namespace NerdStore.Catalog.Domain
{
    public interface IStockService : IDisposable
    {
        Task<bool> RemoveFromStock(Guid productId, int quantity);
        public Task<bool> RemoveFromStock(RequestItems requestItems);
        Task<bool> AddToStock(Guid productId, int quantity);
        public Task<bool> AddToStock(RequestItems requestItems);
    }
}
