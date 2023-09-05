using System.Text.Json;

namespace Tienda.Application.DTO
{
    public class ErrorResponse
    {
        public int HttpStatusCode { get; private set; }
        public List<ValidationError> ValidationErrors { get; private set; }

        public ErrorResponse(int httpStatusCode, List<ValidationError> validationErrors)
        {
            HttpStatusCode = httpStatusCode;
            ValidationErrors = validationErrors;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }

    public class ValidationError
    {
        //public string PropertyName { get; set; }
        public string ErrorDescription { get; set; }
    }
}
