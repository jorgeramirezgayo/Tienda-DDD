using MediatR;
using Tienda.Application.Queries;
using Tienda.Application.Response;

namespace Tienda.Application.Handlers
{
    public class ClienteQueryHandler : IRequestHandler<ClienteQueryRequest, List<ClienteDTO>>
    {
        private readonly IClienteQueries _clienteQuery;
        public ClienteQueryHandler(IClienteQueries query)
        {
            _clienteQuery = query;
        }
        public async Task<List<ClienteDTO>> Handle(ClienteQueryRequest request, CancellationToken cancellationToken)
        {
            var clientes = await _clienteQuery.GetClientesAsync(request);

            return clientes.ToList();
        }
    }
}
