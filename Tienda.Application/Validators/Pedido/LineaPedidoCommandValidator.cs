using FluentValidation;
using Tienda.Application.Models;

namespace Tienda.Application.Validators
{
    public class LineaPedidoCommandValidator : AbstractValidator<OrderItem>
    {
        public LineaPedidoCommandValidator()
        {
            RuleFor(command => command.Cantidad).GreaterThan(0);
            RuleFor(command => command.ProductoId).GreaterThan(0);
        }
    }
}
