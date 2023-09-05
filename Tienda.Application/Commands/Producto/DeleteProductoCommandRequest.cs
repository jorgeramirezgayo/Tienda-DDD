using MediatR;

namespace Tienda.Application.Commands
{
    public class DeleteProductoCommandRequest : IRequest<bool>
    {
        public int Id { get; private set; }

        public DeleteProductoCommandRequest(int id)
        {
            Id = id;
        }
    }
}
