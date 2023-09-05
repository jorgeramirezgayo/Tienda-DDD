using MediatR;
using Tienda.Application.Queries;
using Tienda.Application.Response;

namespace Tienda.Application.Handlers
{
    public class PedidoQueryHandler : IRequestHandler<PedidoQueryRequest, List<PedidoDTO>>
    {
        private readonly IPedidoQueries _pedidoQuery;
        public PedidoQueryHandler(IPedidoQueries query)
        {
            _pedidoQuery = query;
        }
        public async Task<List<PedidoDTO>> Handle(PedidoQueryRequest request, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoQuery.GetPedidosAsync(request);
            return pedido.ToList();
        }
    }
}
