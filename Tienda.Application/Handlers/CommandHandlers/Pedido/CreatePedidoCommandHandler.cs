using AutoMapper;
using MediatR;
using Tienda.Application.Commands;
using Tienda.Application.Exceptions;
using Tienda.Application.Mappers;
using Tienda.Application.Response;
using Tienda.Application.Validators;
using Tienda.Domain.AggregatesModel;

namespace Tienda.Application.Handlers
{
    public class CreatePedidoCommandHandler : IRequestHandler<CreatePedidoCommandRequest, PedidoDTO>
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;
        public CreatePedidoCommandHandler(IPedidoRepository pedidoRepository, IProductoRepository productoRepository, IMapper mapper)
        {
            _pedidoRepository = pedidoRepository;
            _productoRepository = productoRepository;
            _mapper = mapper;
        }
        public async Task<PedidoDTO> Handle(CreatePedidoCommandRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreatePedidoCommandValidator();
            if (validator.Validate(request).IsValid)
            {
                Pedido pedidoEntity = new Pedido(request.ClienteId);

                // Cuando hagamos los get, implementamos el total
                double total = 0;

                foreach (var item in request.OrderItems)
                {
                    var producto = await _productoRepository.GetByIdAsync(item.ProductoId);

                    if (producto != null)
                    {
                        total += producto.Precio * item.Cantidad;
                    }
                }

                pedidoEntity.SetTotal(total);

                foreach (var item in request.OrderItems)
                {
                    pedidoEntity.AniadirLineaPedido(item.Cantidad, item.ProductoId);
                }

                _pedidoRepository.Add(pedidoEntity);
                await _pedidoRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                try
                {
                    PedidoDTO pedidoDTO = _mapper.Map<PedidoDTO>(pedidoEntity);

                    return pedidoDTO;
                }
                catch (Exception error)
                {
                    await Console.Out.WriteLineAsync(error.Message);
                }
                return null;
            }
            else
            {
                throw new InvalidModelException(ErrorParser.ParseFluentValidationErrorList(validator.Validate(request).Errors));
            }
        }
    }
}