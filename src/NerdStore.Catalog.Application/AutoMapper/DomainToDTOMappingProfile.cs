using AutoMapper;
using NerdStore.Catalog.Application.DTOs;
using NerdStore.Catalog.Domain;

namespace NerdStore.Catalog.Application.AutoMapper
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(pdto => pdto.Height, o => o.MapFrom(p => p.Dimension.Height))
                .ForMember(pdto => pdto.Width, o => o.MapFrom(p => p.Dimension.Width))
                .ForMember(pdto => pdto.Depth, o => o.MapFrom(p => p.Dimension.Depth));

            CreateMap<Category, CategoryDTO>();
        }
    }
}
