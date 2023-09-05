using FluentValidation;
using Tienda.Application.Commands;

namespace Tienda.Application.Validators
{
    public class CreatePedidoCommandValidator : AbstractValidator<CreatePedidoCommandRequest>
    {
        public CreatePedidoCommandValidator() {
            RuleFor(command => command.ClienteId).GreaterThan(0);
        }
    }
}
