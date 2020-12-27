using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SportStore.Microservice.Basket.Application.Interfaces;
using SportStore.Microservice.Basket.Application.Mapper.Config;
using SportStore.Microservice.Basket.Application.Service;
using SportStore.Microservice.Basket.Domain.Aggregate.Basket.Interfaces.Service;
using SportStore.Microservice.Basket.Domain.Aggregate.Basket.Repositories.Interfaces;
using SportStore.Microservice.Basket.Domain.Aggregate.Basket.Services;
using SportStore.Microservice.Basket.Domain.Aggregate.Basket.Validator;
using SportStore.Microservice.Basket.Domain.Interfaces;
using SportStore.Microservice.Basket.Infra.Data.Repositories.Redis;
using SportStore.Microservice.Basket.MessageBroker;

namespace SportStore.Microservice.Basket.Infra.CrossCutting
{
    public static class Injections
    {
        public static void AddRegisterServicesBasket(this IServiceCollection services)
        {
            //AutoMapper
            //services.AddSingleton<AutoMapper.IConfigurationProvider>(AutoMapperConfig.RegisterMappings());
            //services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService));
            services.AddSingleton<IConfigurationProvider>(AutoMapperConfig.RegisterMappings());

            //Applications
            services.AddTransient<IBasketAppService, BasketAppService>();

            //Services
            services.AddTransient<IBasketService, BasketService>();

            //Notification
            services.AddSingleton<BasketValidator>();

            //MessageBroker
            services.AddScoped<IMessageBroker, AzureServiceBusQueue>();

            //Repositories Redis
            services.AddScoped<IBasketRepository, BasketRepository>();
        }
    }
}
