using MediatR;
using Tienda.Application.Response;

namespace Tienda.Application.Commands
{
    public class UpdateProductoCommandRequest : IRequest<ProductoDTO>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
    }
}
