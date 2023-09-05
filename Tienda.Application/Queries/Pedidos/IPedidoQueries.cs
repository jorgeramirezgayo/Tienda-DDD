using Tienda.Application.Response;

namespace Tienda.Application.Queries
{
    public interface IPedidoQueries
    {
        Task<IEnumerable<PedidoDTO>> GetPedidosAsync(PedidoQueryRequest request);
    }
}
