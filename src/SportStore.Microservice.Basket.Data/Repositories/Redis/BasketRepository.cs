using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using SportStore.Microservice.Basket.Domain.Aggregate.Basket.Repositories.Interfaces;
using System.Threading.Tasks;

namespace SportStore.Microservice.Basket.Infra.Data.Repositories.Redis
{
    public class BasketRepository : BaseRedisRepository<Domain.Aggregate.Basket.Basket, string>, IBasketRepository
    {
        private readonly IDistributedCache _cacheRedis;

        public BasketRepository(IDistributedCache distributedCache) : base(distributedCache)
        {
            _cacheRedis = distributedCache;
        }

        public override async Task Set(Domain.Aggregate.Basket.Basket dadosCache)
        {
            var dadosJson = JsonConvert.SerializeObject(
                dadosCache,
                Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            await _cacheRedis.SetStringAsync(
                $"{nameof(BasketRepository)}:{dadosCache.UserId}", dadosJson);
        }
    }
}