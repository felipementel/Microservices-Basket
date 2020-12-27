using FluentValidation.Results;

namespace SportStore.Microservice.Basket.Application.DTO.Base
{
    public abstract class BaseDTO
    {
        [System.Text.Json.Serialization.JsonIgnore]
        public ValidationResult ValidationResult { get; set; }

        protected BaseDTO()
        {
            ValidationResult = new ValidationResult();
        }
    }

    public abstract class BaseDTOEntity<Tid> : BaseDTO
    {
        public Tid UserId { get; set; }
    }
}
