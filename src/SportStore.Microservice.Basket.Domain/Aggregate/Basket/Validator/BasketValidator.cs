using FluentValidation;

namespace SportStore.Microservice.Basket.Domain.Aggregate.Basket.Validator
{
    public class BasketValidator : AbstractValidator<Basket>
    {
        public BasketValidator()
        {
            RuleSet("new", () =>
            {
                RuleFor(n => n.UserId)
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ser informado. A cesta de compras precisa pertencer a um usuário.")
                .NotEmpty()
                .WithMessage("É preciso informar um usuário.");

                RuleFor(n => n.Products)
                .NotEmpty()
                .WithMessage("Não identificamos produtos para serem adicionados ao carrinho de compras.");
            });

            RuleSet("update", () =>
            {
                RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("No processo de atualização, não se deve informar o id no corpo da mensagem, apenas do header do request");
            });
        }
    }
}
