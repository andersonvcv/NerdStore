using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalogo.Domain
{
    internal class Category : Entity
    {
        public string Name { get; set; }
        public int Code { get; set; }

        public Category(string name, int code)
        {
            Name = name;
            Code = code;
        }

        public override string ToString()
        {
            return $"{Name} - {Code}";
        }

    }
}
