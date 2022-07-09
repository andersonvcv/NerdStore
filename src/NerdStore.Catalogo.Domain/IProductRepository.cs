using NerdStore.Core.Data;

namespace NerdStore.Catalog.Domain
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetById(Guid id);
        Task<IEnumerable<Product>> GetByCategory(Guid categoryId);

        Task<IEnumerable<Category>> GetCategories();

        void Add(Product product);
        void Update(Product product);
        void Remove(Product product);

        void Add(Category category);
        void Update(Category category);
        void Remove(Category category);
    }
}
