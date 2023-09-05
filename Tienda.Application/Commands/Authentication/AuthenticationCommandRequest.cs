using MediatR;

namespace Tienda.Application.Commands
{
    public class AuthenticationCommandRequest : IRequest<string>
    {
        public string User { get; set; }
        public string Password { get; set; }
    }
}
