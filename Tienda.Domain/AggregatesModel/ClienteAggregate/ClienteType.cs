using Tienda.Domain.SeedWork;

namespace Tienda.Domain.AggregatesModel
{
    public class ClienteType : Enumeration
    {
        public static ClienteType Premium = new ClienteType(1, nameof(Premium).ToLowerInvariant());
        public static ClienteType Standard = new ClienteType(2, nameof(Standard).ToLowerInvariant());

        public ClienteType(int id, string name) : base(id, name)
        {
        }

        public static IEnumerable<ClienteType> List() => new[] { Premium, Standard };
    }
}
