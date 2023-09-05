using FluentValidation;
using Tienda.Application.Commands;

namespace Tienda.Application.Validators
{
    public class CreateClienteCommandValidator : AbstractValidator<CreateClienteCommandRequest>
    {
        public CreateClienteCommandValidator() {
            Include(new BaseClienteCommandValidator());
        }
    }
}
