using MediatR;
using Tienda.Application.Commands;
using Tienda.Application.Common.Enum;
using Tienda.Application.Queries;
using Tienda.Domain.Events;

namespace Tienda.Application.Handlers
{
    public class ChangePriceWhenUpdateProductoDomainEventHandler : INotificationHandler<ChangePriceWhenUpdateProductoDomainEvent>
    {
        private readonly IMediator _mediator;

        public ChangePriceWhenUpdateProductoDomainEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Handle(ChangePriceWhenUpdateProductoDomainEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"El precio del producto {notification.ProductoId} ha sido actualizado.");

            var pedidos = await _mediator.Send(new PedidoQueryRequest(QueryType.ByIdProducto, notification.ProductoId));

            double total;

            foreach (var pedido in pedidos)
            {
                total = 0;

                foreach (var lineaPedido in pedido.OrderItems)
                {
                    var producto = await _mediator.Send(new ProductoQueryRequest(lineaPedido.ProductoId));

                    total += producto.First().Precio * lineaPedido.Cantidad;
                }

                UpdatePedidoCommandRequest updatePedidoCommandRequest = new UpdatePedidoCommandRequest();
                updatePedidoCommandRequest.Id = pedido.Id;
                updatePedidoCommandRequest.ClienteId = pedido.ClienteId;
                updatePedidoCommandRequest.OrderItems = pedido.OrderItems;
                updatePedidoCommandRequest.Total = total;

                var response = await _mediator.Send(updatePedidoCommandRequest);
            }
        }
    }
}
