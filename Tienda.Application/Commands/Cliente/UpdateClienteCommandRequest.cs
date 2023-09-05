using MediatR;
using Tienda.Application.Models;
using Tienda.Application.Response;

namespace Tienda.Application.Commands
{
    public class UpdateClienteCommandRequest : BaseClienteCommandRequest, IRequest<ClienteDTO>
    {
        public int Id { get; set; }
    }
}
