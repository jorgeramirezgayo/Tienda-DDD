using FluentValidation;
using Tienda.Application.Commands;

namespace Tienda.Application.Validators
{
    public class UpdateProductoCommandValidator : AbstractValidator<UpdateProductoCommandRequest>
    {
        public UpdateProductoCommandValidator()
        {
            RuleFor(command => command.Id).NotNull().GreaterThan(0);
            RuleFor(command => command.Nombre).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(command => command.Precio).NotNull().NotEmpty().GreaterThan(0);
        }
    }
}
