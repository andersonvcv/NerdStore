using System.Runtime.InteropServices.ComTypes;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain.Tests
{
    public class ProductTests
    {
        [Fact]
        public void ValidateShouldThrowExceptions()
        {
            var ex = Assert.Throws<DomainException>(() => new Product(string.Empty, "Description", false, 100, Guid.NewGuid(), DateTime.Now, "Image", new Dimension(1, 1, 1)));
            Assert.Equal("Product must have a name", ex.Message);
        }
    }
}