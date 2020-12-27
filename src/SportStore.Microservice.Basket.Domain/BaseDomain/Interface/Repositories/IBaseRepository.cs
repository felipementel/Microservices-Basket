using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportStore.Microservice.Basket.Domain.BaseDomain.Interface.Repositories
{
    public interface IBaseRepository<TEntity, Tid> : IDisposable where TEntity : class
    {
        //Repositório é APENAS um CRUD
        Task Set(TEntity cliente);

        Task Setm(IEnumerable<TEntity> clientes);

        Task<IEnumerable<TEntity>> Getm();

        Task<TEntity> Get(Tid key);

        Task Remove(Tid key);
    }
}