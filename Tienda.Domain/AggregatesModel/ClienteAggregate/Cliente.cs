using System.Text.RegularExpressions;
using Tienda.Domain.Exceptions;
using Tienda.Domain.SeedWork;

namespace Tienda.Domain.AggregatesModel
{
    public class Cliente : Entity, IAggregateRoot
    {
        public string Nombre { get; private set; }
        public string Telefono { get; private set; }

        public Direccion Direccion { get; private set; }

        // Enumeration class
        public ClienteType ClienteType { get; private set; }
        private int _clienteTypeId;

        public Cliente() { }

        public Cliente (string nombre, string telefono, Direccion direccion)
        {
            SetName(nombre);


            Nombre = nombre;
            Telefono = telefono;
            Direccion = direccion;
            SetType(ClienteType.Standard.Id);
        }

        public void SetName(string nombre)
        {
            if (nombre == null || nombre == "")
            {
                throw new BadRequestDomainException("El cliente debe tener obligatoriamente un nombre.");
            }
            else if (nombre.Contains("%@~$/()'"))
            {
                throw new BadRequestDomainException("El nombre de cliente no debe contener caracteres especiales.");
            }
            else if (nombre.Length > 100)
            {
                throw new BadRequestDomainException("El nombre de cliente debe ser menor de 100 caracteres.");
            } else
            {
                Nombre = nombre;
            }
        }

        public void SetTelefono(string telefono)
        {
            if (telefono == null || telefono.Length > 15 || !Regex.IsMatch(telefono, @"^[0-9]+$"))
            {
                throw new BadRequestDomainException("El número de teléfono no es válido.");
            } else
            {
                Telefono = telefono;
            }
        }

        public void SetDireccion(Direccion direccion)
        {
            Direccion = direccion;
        }

        public void SetType(int type)
        {
            IEnumerable<ClienteType> types = Enumeration.GetAll<ClienteType>();
            var existingType = types.Where(d => d.Id == type).SingleOrDefault();
            if (existingType == null)
                throw new BadRequestDomainException($"Invalid value for {nameof(type)} field");
            else
            {
                _clienteTypeId = type;
                ClienteType = Enumeration.FromValue<ClienteType>(_clienteTypeId);
            }
        }
    }
}
