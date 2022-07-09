using AutoMapper;
using NerdStore.Catalog.Application.DTOs;
using NerdStore.Catalog.Domain;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Application.Services
{
    public class ProductApplicationService : IProductApplicationService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IStockService _stockService;

        public ProductApplicationService(IProductRepository productRepository, IMapper mapper, IStockService stockService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _stockService = stockService;
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            return _mapper.Map<IEnumerable<ProductDTO>>(await _productRepository.GetProducts());
        }

        public async Task<ProductDTO> GetById(Guid id)
        {
            return _mapper.Map<ProductDTO>(await _productRepository.GetById(id));
        }

        public async Task<IEnumerable<ProductDTO>> GetByCategory(int code)
        {
            return _mapper.Map<IEnumerable<ProductDTO>>(await _productRepository.GetByCategory(code));
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            return _mapper.Map<IEnumerable<CategoryDTO>>(await _productRepository.GetCategories());
        }

        public async void Add(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            _productRepository.Add(product);

            await _productRepository.UnitOfWork.Commit();
        }

        public async void Update(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            _productRepository.Update(product);

            await _productRepository.UnitOfWork.Commit();
        }

        public async void Remove(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            _productRepository.Remove(product);

            await _productRepository.UnitOfWork.Commit();
        }

        public async Task<ProductDTO> RemoveFromStock(Guid productId, int quantity)
        {
            if(!(await _stockService.RemoveFromStock(productId, quantity)))
            {
                throw new DomainException("Error removing product from stock");
            }

            return _mapper.Map<ProductDTO>(await _productRepository.GetById(productId));
        }

        public async Task<ProductDTO> AddToFromStock(Guid productId, int quantity)
        {
            if (!(await _stockService.AddToStock(productId, quantity)))
            {
                throw new DomainException("Error adding product from stock");
            }

            return _mapper.Map<ProductDTO>(await _productRepository.GetById(productId));
        }

        public void Dispose()
        {
            _productRepository.Dispose();
            _stockService.Dispose();
        }
    }
}
