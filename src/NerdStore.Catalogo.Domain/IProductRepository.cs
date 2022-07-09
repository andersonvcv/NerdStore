using NerdStore.Core.Data;

namespace NerdStore.Catalog.Domain
{
    internal interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetById(Guid id);
        Task<IEnumerable<Product>> GetByCategory(int categoryId);

        Task<IEnumerable<Category>> GetCategories();

        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);

        void Add(Category category);
        void Update(Category category);
        void Delete(Category category);
    }
}
