using Tienda.Application.Models;

namespace Tienda.Application.Commands
{
    public class BaseClienteCommandRequest
    {
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public Address Direccion { get; set; }
    }
}
