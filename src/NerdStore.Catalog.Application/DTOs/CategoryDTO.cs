using System.ComponentModel.DataAnnotations;

namespace NerdStore.Catalog.Application.DTOs
{
    public  class CategoryDTO
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Field {0} is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Field {0} is required")]
        public int Code { get; set; }
    }
}
