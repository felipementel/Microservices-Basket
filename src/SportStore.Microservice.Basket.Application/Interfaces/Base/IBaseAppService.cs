using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportStore.Microservice.Basket.Application.Interfaces
{
    public interface IBaseAppService<TDTO, Tid> where TDTO : class
    {
        Task<TDTO> AddAsync(TDTO itemDTO);
        Task<TDTO> UpdateAsync(Tid Tid, TDTO itemDTO);
        Task<IEnumerable<TDTO>> GetAllAsync();
        Task<bool> RemoverAsync(Tid Tid);
    }
}