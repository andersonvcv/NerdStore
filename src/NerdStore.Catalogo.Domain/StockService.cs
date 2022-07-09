namespace NerdStore.Catalog.Domain
{
    internal class StockService : IStockService
    {
        private readonly IProductRepository _productRepository;

        public StockService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> RemoveFromStock(Guid productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);
            if (product is null || !product.HasAvailableInStock(quantity)) return false;

            product.RemoveFromStock(quantity);

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
