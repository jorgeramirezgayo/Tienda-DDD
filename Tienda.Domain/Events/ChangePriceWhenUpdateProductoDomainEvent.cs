using MediatR;

namespace Tienda.Domain.Events
{
    public class ChangePriceWhenUpdateProductoDomainEvent : INotification
    {
        public int ProductoId { get; }

        public ChangePriceWhenUpdateProductoDomainEvent(int productoId)
        {
            ProductoId = productoId;
        }
    }
}
