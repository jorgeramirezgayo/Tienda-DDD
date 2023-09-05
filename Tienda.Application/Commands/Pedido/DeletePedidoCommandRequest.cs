using MediatR;

namespace Tienda.Application.Commands
{
    public class DeletePedidoCommandRequest : IRequest<bool>
    {
        public int Id { get; private set; }

        public DeletePedidoCommandRequest(int id)
        {
            Id = id;
        }
    }
}
