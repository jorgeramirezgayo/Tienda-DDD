using Tienda.Domain.Exceptions;
using Tienda.Domain.SeedWork;

namespace Tienda.Domain.AggregatesModel
{
    public class Pedido : Entity, IAggregateRoot
    {
        public DateTime Fecha { get; private set; }
        public double Total { get; private set;}

        public int ClienteId => _clienteId;
        private int _clienteId;


        private readonly List<LineaPedido> _orderItems;
        public IReadOnlyCollection<LineaPedido> OrderItems => _orderItems;

        public Pedido()
        {
            _orderItems = new List<LineaPedido>();
        }

        public Pedido (int customerId)
        {
            if (customerId <= 0)
            {
                throw new BadRequestDomainException("El id debe ser mayor que 0.");
            }
            Fecha = DateTime.Now;
            _clienteId = customerId;
            _orderItems = new List<LineaPedido>();
        }

        public void AniadirLineaPedido(int cantidad, int productoId)
        {
            var orderItem = new LineaPedido(cantidad, productoId);
            _orderItems.Add(orderItem);
        }
        public void EliminarTodasLineaPedido()
        {
            _orderItems.Clear();
        }

        public void SetTotal (double total)
        {
            if (total >= 0)
            {
                Total = total;
            }
            else
            {
                throw new BadRequestDomainException("El total debe ser mayor o igual que 0.");
            }
        }

        public void SetClienteId(int clienteId)
        {
            if (clienteId > 0)
            {
                _clienteId = clienteId;
            } else
            {
                throw new BadRequestDomainException("El id de cliente debe ser mayor que 0.");
            }
        }
    }
}
