using AutoMapper;
using SportStore.Microservice.Basket.Application.DTO;
using SportStore.Microservice.Basket.Domain.Aggregate.Basket;

namespace SportStore.Micrsoservice.Basket.Application.Mapper.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            //Product
            CreateMap<ProductDTO, Product>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ReverseMap();
        }
    }
}
