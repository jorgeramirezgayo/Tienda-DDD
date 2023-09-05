using MediatR;
using Tienda.Application.Models;
using Tienda.Application.Response;

namespace Tienda.Application.Commands
{
    public class CreateClienteCommandRequest : BaseClienteCommandRequest, IRequest<ClienteDTO>
    {

    }
}
