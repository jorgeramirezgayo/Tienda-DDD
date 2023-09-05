using FluentValidation;
using Tienda.Application.Commands;

namespace Tienda.Application.Validators
{
    public class AuthenticationCommandValidator : AbstractValidator<AuthenticationCommandRequest>
    {
        public AuthenticationCommandValidator()
        {
            RuleFor(command => command.User).NotNull().NotEmpty();
            RuleFor(command => command.Password).NotNull().NotEmpty();
        }
    }
}
