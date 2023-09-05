using Tienda.Application.Response;

namespace Tienda.Application.Queries
{
    public interface IClienteQueries
    {
        Task<IEnumerable<ClienteDTO>> GetClientesAsync(ClienteQueryRequest request);
    }
}
