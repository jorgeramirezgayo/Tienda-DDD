using FluentValidation;
using Tienda.Application.Commands;

namespace Tienda.Application.Validators
{
    public class CreateProductoCommandValidator : AbstractValidator<CreateProductoCommandRequest>
    {
        public CreateProductoCommandValidator() {
            RuleFor(command => command.Nombre).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(command => command.Precio).NotNull().NotEmpty().GreaterThan(0);
        }
    }
}
