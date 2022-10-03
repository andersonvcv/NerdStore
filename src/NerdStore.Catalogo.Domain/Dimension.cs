using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain
{
    public class Dimension
    {
        public decimal Height { get; set; }
        public decimal Width { get; set; }
        public decimal Depth { get; set; }

        public Dimension(decimal height, decimal width, decimal depth)
        {
            Height = height;
            Width = width;
            Depth = depth;

            Validate();
        }

        public string FormattedDescription()
        {
            return $"HxWxD: {Height} x {Width} x {Depth}";
        }

        public override string ToString()
        {
            return FormattedDescription();
        }

        public void Validate()
        {
            if (Height < 1) throw new DomainException("Height must be larger than 1");
            if (Width < 1) throw new DomainException("Width must be larger than 1");
            if (Depth < 1) throw new DomainException("Depth must be larger than 1");
        }
    }
}
