using MediatR;

namespace Tienda.Application.Commands
{
    public class DeleteClienteCommandRequest : IRequest<bool>
    {
        public int Id { get; private set; }

        public DeleteClienteCommandRequest(int id)
        {
            Id = id;
        }
    }
}
