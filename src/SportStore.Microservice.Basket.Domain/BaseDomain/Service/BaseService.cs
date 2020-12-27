using FluentValidation;
using SportStore.Microservice.Basket.Domain.BaseDomain.Interface.Repositories;
using SportStore.Microservice.Basket.Domain.BaseDomain.Interface.Services;
using SportStore.Microservice.Basket.Domain.BaseDomain.Model;
using SportStore.Microservice.Basket.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportStore.Microservice.Basket.Domain.BaseDomain.Service
{
    public abstract class BaseService<TEntity, Tid> : IBaseService<TEntity, Tid>
        where TEntity : BaseModel<Tid>
    {
        private IBaseRepository<TEntity, Tid> _redisRepository;

        protected AbstractValidator<TEntity> _validator;

        protected BaseService(
            AbstractValidator<TEntity> validator,
            IBaseRepository<TEntity, Tid> redisRepository,
            IMessageBroker messageBroker)
        {
            _validator = validator;
            _redisRepository = redisRepository;
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            if (_validator == null)
            {
                throw new ArgumentException($"Não foi informado o validador da classe {nameof(Basket)}");
            }

            var validated = await _validator.ValidateAsync(entity, options =>
            {
                options.IncludeRuleSets("new");
            });

            entity.ValidationResult = validated;

            if (!validated.IsValid)
            {
                return entity;
            }

            await _redisRepository.Set(entity);

            return entity;
        }

        public virtual async Task<bool> Delete(Tid tid)
        {
            await _redisRepository.Remove(tid);

            return true;
        }

        public virtual async Task<TEntity> Get(Tid tid)
        {
            return await _redisRepository.Get(tid);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _redisRepository.Getm();
        }

        public virtual async Task<TEntity> Update(Tid tid, TEntity entity)
        {
            if (_validator == null)
            {
                throw new ArgumentException($"Não foi informado o validador da classe {nameof(Basket)}");
            }

            var validated = await _validator.ValidateAsync(entity, options =>
            {
                options.IncludeRuleSets("new", "update");
            });

            entity.ValidationResult = validated;

            if (!validated.IsValid)
            {
                return entity;
            }

            await _redisRepository.Set(entity);

            return null;
        }

        public void Dispose()
        {
            _redisRepository = null;
        }
    }
}
