using MediatR;
using Tienda.Application.Commands;
using Tienda.Application.Exceptions;
using Tienda.Application.Mappers;
using Tienda.Application.Validators;
using Tienda.Domain.AggregatesModel;

namespace Tienda.Application.Handlers
{
    public class DeleteProductoCommandHandler : IRequestHandler<DeleteProductoCommandRequest, bool>
    {
        private readonly IProductoRepository _productoRepository;

        public DeleteProductoCommandHandler(IProductoRepository repository)
        {
            _productoRepository = repository;
        }

        public async Task<bool> Handle(DeleteProductoCommandRequest request, CancellationToken cancellationToken)
        {
            var validator = new DeleteProductoCommandValidator();
            if (validator.Validate(request).IsValid)
            {
                var productoEntity = await _productoRepository.GetByIdAsync(request.Id);

                if (productoEntity == null)
                    return false;

                _productoRepository.Delete(productoEntity);
                await _productoRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                return true;
            }
            else
            {
                throw new InvalidModelException(ErrorParser.ParseFluentValidationErrorList(validator.Validate(request).Errors));
            }
        }
    }
}
