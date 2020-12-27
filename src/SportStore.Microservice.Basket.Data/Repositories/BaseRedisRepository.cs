using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using SportStore.Microservice.Basket.Domain.BaseDomain.Interface.Repositories;
using SportStore.Microservice.Basket.Domain.BaseDomain.Model;
using SportStore.Microservice.Basket.Infra.Data.Repositories.Redis;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportStore.Microservice.Basket.Infra.Data.Repositories
{
    public class BaseRedisRepository<TEntity, Tid> : IBaseRepository<TEntity, Tid> where TEntity : BaseModel<Tid>
    {
        private IDistributedCache _cacheRedis;

        public BaseRedisRepository(IDistributedCache cacheRedis)
        {
            _cacheRedis = cacheRedis;
        }

        public virtual async Task<TEntity> Get(Tid key)
        {
            var dadosCache = await _cacheRedis.GetStringAsync(
                $"{nameof(BasketRepository)}:{key}");

            if (!string.IsNullOrEmpty(dadosCache))
            {
                return JsonConvert.DeserializeObject<TEntity>(dadosCache);
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<TEntity>> Getm()
        {
            var dadosCache = await _cacheRedis.GetStringAsync(
                $"{nameof(BasketRepository)}");

            if (!string.IsNullOrEmpty(dadosCache))
            {
                return JsonConvert.DeserializeObject<IEnumerable<TEntity>>(dadosCache);
            }
            else
            {
                return null;
            }
        }

        public async Task Remove(Tid key)
        {
            _cacheRedis.Remove($"{nameof(BasketRepository)}:{key}");
        }

        public virtual async Task Set(TEntity dadosCache)
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

        public async Task Setm(IEnumerable<TEntity> dadosCache)
        {
            var dadosJson = JsonConvert.SerializeObject(
                dadosCache,
                Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            await _cacheRedis.SetStringAsync(
                $"{nameof(BasketRepository)}", dadosJson);
        }

        public void Dispose()
        {
            _cacheRedis = null;
        }
    }
}