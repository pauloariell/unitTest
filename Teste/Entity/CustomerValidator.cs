using FluentValidation;

namespace Teste.Entity
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Campo Obrigatorio");
            RuleFor(x => x.Name).NotEmpty().Length(2,30);
            RuleFor(x => x.Document).NotEmpty().Length(11,14);
        }
    }
}
