using Tienda.Domain.Exceptions;
using Tienda.Domain.SeedWork;

namespace Tienda.Domain.AggregatesModel
{
    public class Producto : Entity, IAggregateRoot
    {
        public string Nombre { get; private set; }
        public double Precio { get; private set; }

        public Producto() { }

        public Producto(string nombre, double precio)
        {
            SetName(nombre);
            SetPrecio(precio);
        }

        public void SetName(string nombre)
        {
            if (nombre == null)
            {
                throw new BadRequestDomainException("El producto debe tener obligatoriamente un nombre.");
            }
            else if (nombre.Contains("%@~$/()'"))
            {
                throw new BadRequestDomainException("El producto no debe contener caracteres especiales.");
            }
            else if (nombre.Length > 100)
            {
                throw new BadRequestDomainException("El nombre de producto debe ser menor de 100 caracteres.");
            } else
            {
                Nombre = nombre;
            }
        }

        public void SetPrecio(double precio)
        {
            if (precio < 0)
            {
                throw new BadRequestDomainException("El precio debe ser mayor de 0.");
            } else
            {
                Precio = precio;
            }
        }
    }
}
