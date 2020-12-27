using AutoMapper;
using SportStore.Microservice.Basket.Application.Interfaces;
using SportStore.Microservice.Basket.Domain.BaseDomain.Interface.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportStore.Microservice.Basket.Application.Service.Base
{
    public abstract class BaseAppService<TEntity, DTO, Tid> : IBaseAppService<DTO, Tid>
        where TEntity : class
        where DTO : class
    {
        protected readonly IMapper _mapper;
        protected readonly IBaseService<TEntity, Tid> _baseService;

        protected BaseAppService(IBaseService<TEntity, Tid> baseService, IMapper mapper)
        {
            _mapper = mapper;
            _baseService = baseService;
        }

        public async virtual Task<DTO> AddAsync(DTO itemDTO)
        {
            var itemMap = _mapper.Map<DTO, TEntity>(itemDTO);
            var item = await _baseService.Add(itemMap);
            itemDTO = _mapper.Map<TEntity, DTO>(item);
            return itemDTO;
        }

        public async Task<DTO> GetAsync(Tid tid)
        {
            var item = await _baseService.Get(tid);
            return _mapper.Map<TEntity, DTO>(item);
        }

        public async Task<IEnumerable<DTO>> GetAllAsync()
        {
            var item = await _baseService.GetAll();
            return _mapper.Map<IEnumerable<TEntity>, IEnumerable<DTO>>(item);
        }

        public async Task<bool> RemoverAsync(Tid tid)
        {
            return await _baseService.Delete(tid);
        }

        public async Task<DTO> UpdateAsync(Tid tid, DTO itemDTO)
        {
            var itemMap = _mapper.Map<DTO, TEntity>(itemDTO);
            var item = await _baseService.Update(tid, itemMap);
            itemDTO = _mapper.Map<TEntity, DTO>(item);

            return itemDTO;
        }
    }
}
