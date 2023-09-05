using MediatR;
using Tienda.Application.Queries;
using Tienda.Application.Response;

namespace Tienda.Application.Handlers
{
    public class ProductoQueryHandler : IRequestHandler<ProductoQueryRequest, List<ProductoDTO>>
    {
        private readonly IProductoQueries _productoQuery;
        public ProductoQueryHandler(IProductoQueries query)
        {
            _productoQuery = query;
        }
        public async Task<List<ProductoDTO>> Handle(ProductoQueryRequest request, CancellationToken cancellationToken)
        {
            if (request.Id != null && request.Id != 0)
            {
                var producto = await _productoQuery.GetProductoByIdAsync(request.Id);
                return producto.ToList();
            }

            var productos = await _productoQuery.GetProductosAsync();
            return productos.ToList();
        }
    }
}
