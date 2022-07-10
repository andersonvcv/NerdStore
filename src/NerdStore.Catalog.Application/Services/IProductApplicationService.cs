using NerdStore.Catalog.Application.DTOs;

namespace NerdStore.Catalog.Application.Services
{
    public interface IProductApplicationService : IDisposable
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
        Task<ProductDTO> GetById(Guid id);
        Task<IEnumerable<ProductDTO>> GetByCategory(int code);

        Task<IEnumerable<CategoryDTO>> GetCategories();

        Task Add(ProductDTO productDTO);
        Task Update(ProductDTO productDTO);
        Task Remove(ProductDTO productDTO);

        Task<ProductDTO> RemoveFromStock(Guid productId, int quantity);
        Task<ProductDTO> AddToStock(Guid productId, int quantity);
    }
}
