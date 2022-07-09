namespace NerdStore.Catalog.Domain
{
    internal interface IStockService : IDisposable
    {
        Task<bool> RemoveFromStock(Guid productId, int quantity);
        Task<bool> AddToStock(Guid productId, int quantity);
    }
}
