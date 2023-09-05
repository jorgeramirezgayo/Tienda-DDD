namespace Tienda.Domain.Exceptions
{
    public class BadRequestDomainException : Exception
    {
        public BadRequestDomainException()
        { }

        public BadRequestDomainException(string message)
            : base(message)
        { }

        public BadRequestDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
