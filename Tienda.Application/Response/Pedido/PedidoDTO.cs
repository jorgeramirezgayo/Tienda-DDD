using Tienda.Application.Models;

namespace Tienda.Application.Response
{
    public class PedidoDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public double Total { get; set; }
        public int ClienteId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
