using FluentValidation;
using Tienda.Application.Commands;

namespace Tienda.Application.Validators
{
    public class UpdatePedidoCommandValidator : AbstractValidator<UpdatePedidoCommandRequest>
    {
        public UpdatePedidoCommandValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0);
            RuleFor(command => command.Total).GreaterThanOrEqualTo(0);
            RuleFor(command => command.ClienteId).GreaterThan(0);
            RuleForEach(command => command.OrderItems).SetValidator(new LineaPedidoCommandValidator());
        }
    }
}
