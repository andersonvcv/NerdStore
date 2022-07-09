using System.ComponentModel.DataAnnotations;

namespace NerdStore.Catalog.Application.DTOs
{
    public class ProductDTO
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Field {0} is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Field {0} is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Field {0} is required")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Field {0} is required")]
        public decimal Value { get; set; }

        [Required(ErrorMessage = "Field {0} is required")]
        public DateTime EntryDate { get; set; }

        [Required(ErrorMessage = "Field {0} is required")]
        public string Image { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Field {0} must have at least {1}")]
        [Required(ErrorMessage = "Field {0} is required")]
        public int QuantityInStock { get; set; }

        [Required(ErrorMessage = "Field {0} is required")]
        public Guid CategoryId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Field {0} must have at least {1}")]
        [Required(ErrorMessage = "Field {0} is required")]
        public int Height { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Field {0} must have at least {1}")]
        [Required(ErrorMessage = "Field {0} is required")]
        public int Width { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Field {0} must have at least {1}")]
        [Required(ErrorMessage = "Field {0} is required")]
        public int Depth { get; set; }

        public IEnumerable<CategoryDTO> Categories { get; set; }
    }
}
