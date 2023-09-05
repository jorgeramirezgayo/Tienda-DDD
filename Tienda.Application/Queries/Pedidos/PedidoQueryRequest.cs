using MediatR;
using Tienda.Application.Common.Enum;
using Tienda.Application.Response;

namespace Tienda.Application.Queries
{
    public class PedidoQueryRequest : IRequest<List<PedidoDTO>>
    {
        public int Id { get; private set; }
        public int ProductoId { get; private set; }
        public QueryType QueryType { get; private set; }
        public PedidoQueryRequest(QueryType queryType, int id)
        {
            QueryType = queryType;
            if (QueryType == QueryType.Todos || QueryType == QueryType.Uno)
            {
                Id = id;
            } else if (QueryType == QueryType.ByIdProducto) 
            {
                ProductoId = id;
            }

        }
    }
}
