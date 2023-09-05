using System.Text.RegularExpressions;
using Tienda.Domain.Exceptions;
using Tienda.Domain.SeedWork;

namespace Tienda.Domain.AggregatesModel
{
    public class Direccion : ValueObject
    {
        public string Calle { get; private set; }
        public string Ciudad { get; private set; }
        public string Provincia { get; private set; }
        public string Pais { get; private set; }
        public string CodigoPostal { get; private set; }

        public Direccion () { }

        public Direccion(string calle, string ciudad, string provincia, string pais, string codigoPostal)
        {
            if (calle == null || calle == "" || calle.Length > 100)
            {
                throw new BadRequestDomainException("La dirección debe tener obligatoriamente una calle.");
            }
            else if (ciudad == null || ciudad == "" || ciudad.Length > 100)
            {
                throw new BadRequestDomainException("La dirección debe tener obligatoriamente una ciudad.");
            }
            else if (provincia == null || provincia == "" || provincia.Length > 50)
            {
                throw new BadRequestDomainException("La dirección debe tener obligatoriamente una provincia.");
            }
            else if (pais == null || pais == "" || pais.Length > 35)
            {
                throw new BadRequestDomainException("La dirección debe tener obligatoriamente un pais.");
            }
            else if (codigoPostal == null || codigoPostal == "" || !Regex.IsMatch(codigoPostal, @"^[0-9]{5}$"))
            {
                throw new BadRequestDomainException("La dirección debe tener obligatoriamente un código postal.");
            }

            Calle = calle;
            Ciudad = ciudad;
            Provincia = provincia;
            Pais = pais;
            CodigoPostal = codigoPostal;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Calle;
            yield return Ciudad;
            yield return Provincia;
            yield return Pais;
            yield return CodigoPostal;
        }
    }
}
