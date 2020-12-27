using AutoMapper;
using SportStore.Microservice.Basket.Application.DTO;
using SportStore.Microservice.Basket.Domain.Aggregate.Basket;

namespace SportStore.Micrsoservice.Basket.Application.Mapper.Profiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            //Basket
            CreateMap<BasketDTO, Microservice.Basket.Domain.Aggregate.Basket.Basket>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products))
                .ReverseMap();
        }
    }
}
