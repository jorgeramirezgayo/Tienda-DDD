using MediatR;
using Tienda.Application.Commands;
using Tienda.Application.Exceptions;
using Tienda.Application.Mappers;
using Tienda.Application.Validators;
using Tienda.Domain.AggregatesModel;

namespace Tienda.Application.Handlers
{
    public class DeleteClienteCommandHandler : IRequestHandler<DeleteClienteCommandRequest, bool>
    {
        private readonly IClienteRepository _clienteRepository;

        public DeleteClienteCommandHandler(IClienteRepository repository)
        {
            _clienteRepository = repository;
        }

        public async Task<bool> Handle(DeleteClienteCommandRequest request, CancellationToken cancellationToken)
        {
            var validator = new DeleteClienteCommandValidator();
            if (validator.Validate(request).IsValid)
            {
                var clienteEntity = await _clienteRepository.GetByIdAsync(request.Id);

                if (clienteEntity == null)
                    return false;

                _clienteRepository.Delete(clienteEntity);
                await _clienteRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                return true;
            }
            else
            {
                throw new InvalidModelException(ErrorParser.ParseFluentValidationErrorList(validator.Validate(request).Errors));
            }
        }
    }
}
