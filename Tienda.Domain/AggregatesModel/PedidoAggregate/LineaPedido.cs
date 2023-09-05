using Tienda.Domain.Exceptions;
using Tienda.Domain.SeedWork;

namespace Tienda.Domain.AggregatesModel
{
    public class LineaPedido : Entity
    {
        public int Cantidad => _cantidad;
        private int _cantidad { get; set; }

        public int ProductoId => _productoId;
        private int _productoId;

        public LineaPedido() { }

        public LineaPedido(int cantidad, int productoId)
        {
            if (cantidad <= 0) {
                throw new BadRequestDomainException("La cantidad debe ser al menos uno.");
            } else if (productoId <= 0) {
                throw new BadRequestDomainException("El id debe ser mayor que 0.");
            }

            _cantidad = cantidad;
            _productoId = productoId;
        }

        public int GetCantidad()
        {
            return _cantidad;
        }
    }
}
