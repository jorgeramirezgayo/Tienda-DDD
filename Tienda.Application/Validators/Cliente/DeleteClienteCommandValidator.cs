using FluentValidation;
using Tienda.Application.Commands;

namespace Tienda.Application.Validators
{
    public class DeleteClienteCommandValidator : AbstractValidator<DeleteClienteCommandRequest>
    {
        public DeleteClienteCommandValidator()
        {
            RuleFor(command => command.Id).NotNull().GreaterThan(0);
        }
    }
}
