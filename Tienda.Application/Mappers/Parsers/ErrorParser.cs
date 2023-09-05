using FluentValidation.Results;
using Tienda.Application.DTO;

namespace Tienda.Application.Mappers
{
    public class ErrorParser
    {
        public static List<ValidationError> ParseFluentValidationErrorList(List<ValidationFailure> fluentValidationErrors)

        {

            var errors = new List<ValidationError>();

            fluentValidationErrors.ForEach(e => errors.Add(new ValidationError()

            {

                // PropertyName = e.PropertyName == string.Empty ? "Id" : e.PropertyName,

                ErrorDescription = e.ErrorMessage.Replace("''", "Id").Replace("'", string.Empty)

            }));

            return errors;

        }

        public static List<ValidationError> ParseSingleError(string msg)
        {
            return new List<ValidationError>() { new ValidationError { ErrorDescription = msg } };
        }

        internal static List<ValidationError> ParseFluentValidationErrorList(object errors)
        {
            throw new NotImplementedException();
        }
    }
}
