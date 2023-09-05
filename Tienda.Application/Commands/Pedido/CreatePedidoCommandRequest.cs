using MediatR;
using Tienda.Application.Models;
using Tienda.Application.Response;

namespace Tienda.Application.Commands
{
    public class CreatePedidoCommandRequest : IRequest<PedidoDTO>
    {
        public int ClienteId { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        internal object Validate(CreatePedidoCommandRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
