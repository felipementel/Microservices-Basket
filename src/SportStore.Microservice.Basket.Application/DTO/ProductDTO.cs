using SportStore.Microservice.Basket.Application.DTO.Base;

namespace SportStore.Microservice.Basket.Application.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }
    }
}
