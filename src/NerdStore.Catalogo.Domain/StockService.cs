using NerdStore.Catalog.Domain.Events;
using NerdStore.Core.MediatR;

namespace NerdStore.Catalog.Domain
{
    internal class StockService : IStockService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMediatRHandler _mediatRHandler;

        public StockService(IProductRepository productRepository, IMediatRHandler mediatRHandler)
        {
            _productRepository = productRepository;
            _mediatRHandler = mediatRHandler;
        }

        public async Task<bool> RemoveFromStock(Guid productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);
            if (product is null || !product.HasAvailableInStock(quantity)) return false;

            product.RemoveFromStock(quantity);

            if (product.QuantityInStock < Constants.LOW_STOCK_TRESHOLD)
            {
                await _mediatRHandler.PublishEvent(new LowStockEvent(product.Id, product.QuantityInStock));
            }

            _productRepository.Update(product);
            return await _productRepository.UnitOfWork.Commit();
        }

        public async Task<bool> AddToStock(Guid productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);
            if (product is null) return false;

            product.AddToStock(quantity);

            _productRepository.Update(product);
            return await _productRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            _productRepository.Dispose();
        }
    }
}
