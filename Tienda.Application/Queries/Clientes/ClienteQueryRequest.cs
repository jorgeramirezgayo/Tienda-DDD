using MediatR;
using Tienda.Application.Common.Enum;
using Tienda.Application.Response;

namespace Tienda.Application.Queries
{
    public class ClienteQueryRequest : IRequest<List<ClienteDTO>>
    {
        public int Id { get; private set; }
        public QueryType QueryType { get; private set; }
        public ClienteQueryRequest(QueryType queryType, int id) 
        {
            QueryType = queryType;
            Id = id;
        }
    }
}
