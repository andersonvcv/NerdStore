using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain
{
    public class Product : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; private set; }
        public decimal Value { get; private set; }
        public DateTime EntryDate { get; set; }
        public string Image { get; private set; }
        public int Quantity { get; private set; }
        public Guid CategoryId { get; private set; }
        public Dimension Dimension { get; private set; }
        public Category Category { get; private set; }

        public Product(string name, string description, bool active, decimal value, Guid categoryId, DateTime entryDate, string image, Dimension dimension)
        {
            Name = name;
            Description = description;
            IsActive = active;
            Value = value;
            CategoryId = categoryId;
            EntryDate = entryDate;
            Image = image;
            Dimension = dimension;

            Validate();
        }

        protected Product()
        {
        }

        public void Active() => IsActive = true;
        
        public void Deactivate() => IsActive = false;

        public void ChangeCategory(Category category)
        {
            Category = category;
            CategoryId = category.Id;
        }

        public void ChangeDescription(string description)
        {
            description = description.Trim();
            if (description.Equals(string.Empty)) throw new DomainException("Description must have a value");
            Description = description;
        }

        public void RemoveFromStock(int quantity)
        {
            if (quantity < 0) throw new DomainException("Negative values is not supported");
            if (!HasAvailableInStock(quantity)) throw new DomainException("Insufficient Stock");

            Quantity -= quantity;
        }

        public void AddToStock(int quantity)
        {
            if (quantity < 0)
            {
                throw new DomainException("Negative values is not supported");
            }

            Quantity += quantity;
        }

        public bool HasAvailableInStock(int quantity)
        {
            return Quantity > 0 && Quantity >= quantity;
        }

        public void Validate()
        {
            if (Name.Equals(string.Empty)) throw new DomainException("Product must have a name");
        }
    }
}
