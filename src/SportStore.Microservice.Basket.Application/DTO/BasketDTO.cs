using SportStore.Microservice.Basket.Application.DTO.Base;
using System.Collections.Generic;

namespace SportStore.Microservice.Basket.Application.DTO
{
    public class BasketDTO : BaseDTOEntity<string>
    {
        public List<ProductDTO> Products { get; set; }
    }
}
