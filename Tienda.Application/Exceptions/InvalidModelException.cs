using Tienda.Application.DTO;

namespace Tienda.Application.Exceptions
{
    public class InvalidModelException : Exception
    {
        public List<ValidationError> Errors { get; private set; }
        public InvalidModelException(List<ValidationError> errors) : base()
        {
            this.Errors = errors;
        }
    }
}
