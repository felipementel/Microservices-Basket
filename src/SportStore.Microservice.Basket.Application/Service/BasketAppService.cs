using AutoMapper;
using SportStore.Microservice.Basket.Application.DTO;
using SportStore.Microservice.Basket.Application.Interfaces;
using SportStore.Microservice.Basket.Application.Service.Base;
using SportStore.Microservice.Basket.Domain.Aggregate.Basket.Interfaces.Service;

namespace SportStore.Microservice.Basket.Application.Service
{
    public class BasketAppService : BaseAppService<Domain.Aggregate.Basket.Basket, BasketDTO, string>, IBasketAppService
    {
        protected IBasketService _appService;
        protected IMapper _mapper;

        public BasketAppService(IBasketService appService, IMapper mapper) : base(appService, mapper)
        {
            _appService = appService;
            _mapper = mapper;
        }
    }
}