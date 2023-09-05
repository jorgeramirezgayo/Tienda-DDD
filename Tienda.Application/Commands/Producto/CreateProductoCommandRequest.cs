using MediatR;
using Tienda.Application.Response;

namespace Tienda.Application.Commands
{
    public class CreateProductoCommandRequest : IRequest<ProductoDTO>
    {
        public string Nombre { get; set; }
        public double Precio { get; set; }

    }
}
