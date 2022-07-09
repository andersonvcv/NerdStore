using NerdStore.Catalog.Application.DTOs;

namespace NerdStore.Catalog.Application.Services
{
    internal interface IProductApplicationService : IDisposable
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
        Task<ProductDTO> GetById(Guid id);
        Task<IEnumerable<ProductDTO>> GetByCategory(int code);

        Task<IEnumerable<CategoryDTO>> GetCategories();

        void Add(ProductDTO productDTO);
        void Update(ProductDTO productDTO);
        void Remove(ProductDTO productDTO);

        Task<ProductDTO> RemoveFromStock(Guid productId, int quantity);
        Task<ProductDTO> AddToFromStock(Guid productId, int quantity);
    }
}
