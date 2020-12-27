using FluentValidation;
using SportStore.Microservice.Basket.Domain.Aggregate.Basket.Interfaces.Service;
using SportStore.Microservice.Basket.Domain.Aggregate.Basket.Repositories.Interfaces;
using SportStore.Microservice.Basket.Domain.Aggregate.Basket.Validator;
using SportStore.Microservice.Basket.Domain.BaseDomain.Service;
using SportStore.Microservice.Basket.Domain.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Microservice.Basket.Domain.Aggregate.Basket.Services
{
    public class BasketService : BaseService<Basket, string>, IBasketService
    {
        private readonly IBasketRepository _redisRepository;

        private readonly IMessageBroker _messageBroker;

        protected BasketValidator _validator;

        public BasketService(
            BasketValidator validator,
            IBasketRepository redisRepository,
            IMessageBroker messageBroker)
            : base(validator,
                  redisRepository,
                  messageBroker)
        {
            _redisRepository = redisRepository;
            _messageBroker = messageBroker;
            _validator = validator;
        }

        public override async Task<Basket> Add(Basket entity)
        {
            var basket = await _redisRepository.Get(entity.UserId);

            if (!(basket is null))
            {
                AddItem(ref basket, entity);

                await base.Add(basket);

                _messageBroker.EnQueue<Basket>(new Basket { Products = basket.Products, UserId = basket.UserId }, nameof(BasketService));

                return basket;
            }
            else
            {
                return await base.Add(entity);
            }
        }

        private Basket AddItem(ref Basket basket, Basket entity)
        {
            var item = basket
                .Products
                .Where(item => item.ProductId == entity.Products.First().ProductId)
                .FirstOrDefault();

            if (item != null)
            {
                item.Quantity += entity.Products.First().Quantity;
            }
            else
            {
                basket.Products.Add(entity.Products.First());
            }

            return basket;
        }
    }
}