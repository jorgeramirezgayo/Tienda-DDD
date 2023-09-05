using MediatR;
using Tienda.Application.Authentication;
using Tienda.Application.Authorization;
using Tienda.Application.Commands;
using Tienda.Application.DTO;
using Tienda.Application.Exceptions;
using Tienda.Application.Mappers;
using Tienda.Application.Validators;

namespace Tienda.Application.Handlers
{
    public class AuthenticationCommandHandler : IRequestHandler<AuthenticationCommandRequest, string>
    {
        private readonly IJwtUtils _jwtUtils;
        private readonly IAuthentication _authentication;

        public AuthenticationCommandHandler(IJwtUtils jwt, IAuthentication authentication)
        {
            _jwtUtils = jwt;
            _authentication = authentication;
        }

        public Task<string> Handle(AuthenticationCommandRequest request, CancellationToken cancellationToken)
        {
            var validator = new AuthenticationCommandValidator();
            if (validator.Validate(request).IsValid)
            {
                if (_authentication.Validate(request.User, request.Password))
                {
                    return Task.FromResult(_jwtUtils.GenerateJwtToken(request.User));
                }
                else
                {
                    var validationError = new ValidationError(){ ErrorDescription = "Invalid credentials" };
                    throw new InvalidModelException(new List<ValidationError>() { validationError });
                }
            }
            else
            {
                throw new InvalidModelException(ErrorParser.ParseFluentValidationErrorList(validator.Validate(request).Errors));
            }
        }
    }
}
