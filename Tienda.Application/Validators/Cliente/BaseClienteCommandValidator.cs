using FluentValidation;
using System.Text.RegularExpressions;
using Tienda.Application.Commands;

namespace Tienda.Application.Validators
{
    public class BaseClienteCommandValidator : AbstractValidator<BaseClienteCommandRequest>
    {
        public BaseClienteCommandValidator() 
        {
            RuleFor(command => command.Nombre).NotNull().NotEmpty().MaximumLength(150).Custom((nombre, context) =>
            {
                if (!ValidarNombre(nombre))
                {
                    context.AddFailure("El campo Nombre contiene caracteres inválidos.");
                }
            }); ;
            RuleFor(command => command.Telefono).NotNull().NotEmpty().MaximumLength(15).Matches(new Regex("^[0-9]+$"));
            RuleFor(command => command.Direccion.Calle).NotNull().NotEmpty().MaximumLength(150);
            RuleFor(command => command.Direccion.Ciudad).NotNull().NotEmpty().MaximumLength(150);
            RuleFor(command => command.Direccion.Provincia).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(command => command.Direccion.Pais).NotNull().NotEmpty().MaximumLength(35);
            RuleFor(command => command.Direccion.CodigoPostal).NotNull().NotEmpty().Matches(new Regex(@"^[0-9]{5}$"));
        }

        private bool ValidarNombre(string nombre)
        {
            return Regex.IsMatch(nombre, "^[A-Za-z ]+$");
        }
    }
}
