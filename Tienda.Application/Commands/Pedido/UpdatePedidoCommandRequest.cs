using MediatR;
using Tienda.Application.Models;
using Tienda.Application.Response;

namespace Tienda.Application.Commands
{
    public class UpdatePedidoCommandRequest : IRequest<PedidoDTO>
    {
        public int Id { get; set; }
        public double Total { get; set; }
        public int ClienteId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
