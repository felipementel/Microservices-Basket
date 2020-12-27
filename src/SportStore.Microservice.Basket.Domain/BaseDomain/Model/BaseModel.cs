using FluentValidation.Results;
using System;

namespace SportStore.Microservice.Basket.Domain.BaseDomain.Model
{
    public abstract class BaseModel<Tid>
    {
        protected BaseModel()
        {
            ValidationResult = new ValidationResult();
        }

        public Tid UserId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public ValidationResult ValidationResult { get; set; }
    }
}
