using SportStore.Microservice.Basket.Domain.BaseDomain.Model;
using System.Collections.Generic;

namespace SportStore.Microservice.Basket.Domain.Aggregate.Basket
{
    public class Basket : BaseModel<string>
    {
        public List<Product> Products { get; set; }
    }
}