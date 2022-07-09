using AutoMapper;
using NerdStore.Catalog.Application.DTOs;
using NerdStore.Catalog.Domain;

namespace NerdStore.Catalog.Application.AutoMapper
{
    public class DTOToDomainMappingProfile : Profile
    {
        public DTOToDomainMappingProfile()
        {
            CreateMap<CategoryDTO, Category>()
                .ConstructUsing(cdto => new Category(cdto.Name, cdto.Code));

            CreateMap<ProductDTO, Product>()
                .ConstructUsing(pdto => 
                    new Product(pdto.Name, pdto.Description, pdto.IsActive, pdto.Value, pdto.CategoryId, pdto.EntryDate, pdto.Image, 
                        new Dimension(pdto.Height, pdto.Width, pdto.Depth)));
        }
    }
}
