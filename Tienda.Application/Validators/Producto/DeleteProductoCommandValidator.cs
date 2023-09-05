using FluentValidation;
using Tienda.Application.Commands;

namespace Tienda.Application.Validators
{
    public class DeleteProductoCommandValidator : AbstractValidator<DeleteProductoCommandRequest>
    {
        public DeleteProductoCommandValidator()
        {
            RuleFor(command => command.Id).NotNull().GreaterThan(0);
        }
    }
}
