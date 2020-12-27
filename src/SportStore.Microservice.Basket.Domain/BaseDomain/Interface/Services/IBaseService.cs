using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportStore.Microservice.Basket.Domain.BaseDomain.Interface.Services
{
    public interface IBaseService<TEntity, Tid> : IDisposable where TEntity : class
    {
        Task<TEntity> Get(Tid tid);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(Tid tid, TEntity entity);
        Task<bool> Delete(Tid tid);
    }
}
