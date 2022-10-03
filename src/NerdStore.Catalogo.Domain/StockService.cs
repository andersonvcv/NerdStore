using NerdStore.Catalog.Domain.Events;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.DomainObjects.DTOs;
using NerdStore.Core.Messages.Notifications;
using NerdStore.Sales.Domain;

namespace NerdStore.Catalog.Domain
{
    public class StockService : IStockService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMediatoRHandler _mediatoRHandler;

        public StockService(IProductRepository productRepository, IMediatoRHandler mediatoRHandler)
        {
            _productRepository = productRepository;
            _mediatoRHandler = mediatoRHandler;
        }

        public async Task<bool> RemoveFromStock(Guid productId, int quantity)
        {
            if (!await RemoveItemFromStock(productId, quantity)) return false;

            return await _productRepository.UnitOfWork.Commit();
        }

        public async Task<bool> RemoveFromStock(RequestItems requestItems)
        {
            foreach (var item in requestItems.Items)
            {
                if (!await RemoveItemFromStock(item.Id, item.Quantity)) return false;
            }

            return await _productRepository.UnitOfWork.Commit();
        }

        public async Task<bool> AddToStock(Guid productId, int quantity)
        {
            if (!await AddItemToStock(productId, quantity)) return false;
            return await _productRepository.UnitOfWork.Commit();
        }

        public async Task<bool> AddToStock(RequestItems requestItems)
        {
            foreach (var item in requestItems.Items)
            {
                if (!await AddItemToStock(item.Id, item.Quantity)) return false;
            }

            return await _productRepository.UnitOfWork.Commit();
        }

        private async Task<bool> AddItemToStock(Guid productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);
            if (product is null) return false;

            product.AddToStock(quantity);

            _productRepository.Update(product);
            return true;
        }

        private async Task<bool> RemoveItemFromStock(Guid productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);
            if (product is null) return false;

            if (!product.HasAvailableInStock(quantity))
            {
                await _mediatoRHandler.PublishNotification(new DomainNotification("Stock",
                    $"Product - {product.Name} without stock"));
                return false;
            }

            product.RemoveFromStock(quantity);

            if (product.QuantityInStock < Constants.LOW_STOCK_TRESHOLD)
            {
                await _mediatoRHandler.PublishEvent(new LowStockEvent(product.Id, product.QuantityInStock));
            }

            _productRepository.Update(product);
            return true;
        }

        public void Dispose()
        {
            _productRepository.Dispose();
        }
    }
}
