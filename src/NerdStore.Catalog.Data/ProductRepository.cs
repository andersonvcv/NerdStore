using Microsoft.EntityFrameworkCore;
using NerdStore.Catalog.Domain;
using NerdStore.Core.Data;

namespace NerdStore.Catalog.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogContext _catalogContext;
        public IUnitOfWork UnitOfWork => _catalogContext;
        
        public ProductRepository(CatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _catalogContext.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product?> GetById(Guid id)
        {
            return await _catalogContext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetByCategory(int code)
        {
            return await _catalogContext.Products.AsNoTracking().Include(p => p.Category).Where(p => p.Category.Code == code).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _catalogContext.Categories.AsNoTracking().ToListAsync();
        }

        public void Add(Product product)
        {
            _catalogContext.Products.Add(product);
        }

        public void Update(Product product)
        {
            _catalogContext.Products.Update(product);
        }

        public void Remove(Product product)
        {
            _catalogContext.Products.Remove(product);
        }

        public void Add(Category category)
        {
            _catalogContext.Categories.Add(category);
        }

        public void Update(Category category)
        {
            _catalogContext.Categories.Update(category);
        }

        public void Remove(Category category)
        {
            _catalogContext.Categories.Remove(category);
        }

        public void Dispose()
        {
            _catalogContext.Dispose();
        }
    }
}
