using AutoMapper;
using WebApi.DTOs;
using WebApi.Model;

namespace WebApi.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            //product
            CreateMap<Product, ProductCreateDTO>();
            CreateMap<ProductCreateDTO, Product>();


            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.ProductGroupDTOs, opt => opt.MapFrom(src =>src.ProductGroup));
            CreateMap<ProductDTO, Product>();


            //product group
            CreateMap<ProductGroupCreateDTO, ProductGroup>();

            CreateMap<ProductGroup, ProductGroupDTO>()
               .ForMember(dest => dest.ProductDTOs, opt => opt.MapFrom(src => src.Products));
            CreateMap<ProductGroupDTO, ProductGroup>();
        }
    }
}
