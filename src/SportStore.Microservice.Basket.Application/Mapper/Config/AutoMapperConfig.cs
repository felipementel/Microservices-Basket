using AutoMapper;
using SportStore.Micrsoservice.Basket.Application.Mapper.Profiles;

namespace SportStore.Microservice.Basket.Application.Mapper.Config
{
    public class AutoMapperConfig
    {
        protected AutoMapperConfig() { }

        public static MapperConfiguration RegisterMappings() =>
            new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AddProfile<BasketProfile>();
                cfg.AddProfile<ProductProfile>();
            });
    }
}
