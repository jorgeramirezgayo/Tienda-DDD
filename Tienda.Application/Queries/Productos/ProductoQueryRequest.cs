using MediatR;
using Tienda.Application.Response;

namespace Tienda.Application.Queries
{
    public class ProductoQueryRequest : IRequest<List<ProductoDTO>>
    {
        public int Id { get; private set; }

        public ProductoQueryRequest() { }

        public ProductoQueryRequest(int id) { 
            Id = id;
        }
    }
}
