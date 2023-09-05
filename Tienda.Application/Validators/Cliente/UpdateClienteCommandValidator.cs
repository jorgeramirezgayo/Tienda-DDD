using FluentValidation;
using System.Text.RegularExpressions;
using Tienda.Application.Commands;

namespace Tienda.Application.Validators
{
    public class UpdateClienteCommandValidator : AbstractValidator<UpdateClienteCommandRequest>
    {
        public UpdateClienteCommandValidator()
        {
            Include(new BaseClienteCommandValidator());
            RuleFor(command => command.Id).Must(IsOver0);
        }

        private bool IsOver0(int id)
        {
            if (id >= 0)
            {
                return true;
            }
            return false;
        }
    }
}
