using AutoMapper;
using MediatR;
using Tienda.Application.Commands;
using Tienda.Application.Exceptions;
using Tienda.Application.Mappers;
using Tienda.Application.Models;
using Tienda.Application.Response;
using Tienda.Application.Validators;
using Tienda.Domain.AggregatesModel;

namespace Tienda.Application.Handlers
{
    public class UpdatePedidoCommandHandler : IRequestHandler<UpdatePedidoCommandRequest, PedidoDTO>
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapper _mapper;

        public UpdatePedidoCommandHandler(IPedidoRepository repository, IMapper mapper)
        {
            _pedidoRepository = repository;
            _mapper = mapper;
        }
        public async Task<PedidoDTO> Handle(UpdatePedidoCommandRequest request, CancellationToken cancellationToken)
        {
            var validatorPedido = new UpdatePedidoCommandValidator();
            List<OrderItem> orderItems = request.OrderItems;

            if (validatorPedido.Validate(request).IsValid)
            {

                    var pedidoEntity = await _pedidoRepository.GetByIdAsync(request.Id);

                    if (pedidoEntity == null)
                    {
                        return null;
                    }

                    pedidoEntity.EliminarTodasLineaPedido();
                    pedidoEntity.SetTotal(request.Total);
                    pedidoEntity.SetClienteId(request.ClienteId);
                    foreach (var item in request.OrderItems)
                    {
                        pedidoEntity.AniadirLineaPedido(item.Cantidad, item.ProductoId);
                    }

                    _pedidoRepository.Update(pedidoEntity);
                    await _pedidoRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                    PedidoDTO pedidoDTO = _mapper.Map<PedidoDTO>(pedidoEntity);

                    return pedidoDTO;
            }
            else
            {
                throw new InvalidModelException(ErrorParser.ParseFluentValidationErrorList(validatorPedido.Validate(request).Errors));
            }
        }
    }
}
