using SportStore.Microservice.Basket.Domain.BaseDomain.Model;

namespace SportStore.Microservice.Basket.Domain.Aggregate.Basket
{
    public class Product
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public double Price { get; set; }
    }
}