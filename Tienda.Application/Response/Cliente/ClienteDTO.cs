using Tienda.Domain.AggregatesModel;

namespace Tienda.Application.Response
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public Direccion Direccion { get; set; }
        public int ClienteType { get; set; }
    }
}
