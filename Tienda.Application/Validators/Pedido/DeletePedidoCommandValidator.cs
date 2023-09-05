using FluentValidation;
using Tienda.Application.Commands;

namespace Tienda.Application.Validators
{
    internal class DeletePedidoCommandValidator : AbstractValidator<DeletePedidoCommandRequest>
    {
        public DeletePedidoCommandValidator()
        {
            RuleFor(command => command.Id).NotNull().GreaterThan(0);
        }
    }
}
