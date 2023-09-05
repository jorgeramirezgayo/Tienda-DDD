using MediatR;
using Tienda.Application.Commands;
using Tienda.Application.Exceptions;
using Tienda.Application.Mappers;
using Tienda.Application.Validators;
using Tienda.Domain.AggregatesModel;

namespace Tienda.Application.Handlers
{
    public class DeletePedidoCommandHandler : IRequestHandler<DeletePedidoCommandRequest, bool>
    {
        private readonly IPedidoRepository _pedidoRepository;

        public DeletePedidoCommandHandler(IPedidoRepository repository)
        {
            _pedidoRepository = repository;
        }

        public async Task<bool> Handle(DeletePedidoCommandRequest request, CancellationToken cancellationToken)
        {
            var validator = new DeletePedidoCommandValidator();
            if (validator.Validate(request).IsValid)
            {
                var pedidoEntity = await _pedidoRepository.GetByIdAsync(request.Id);

                if (pedidoEntity == null)
                    return false;

                _pedidoRepository.Delete(pedidoEntity);
                await _pedidoRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                return true;
            }
            else
            {
                throw new InvalidModelException(ErrorParser.ParseFluentValidationErrorList(validator.Validate(request).Errors));
            }
        }
    }
}
