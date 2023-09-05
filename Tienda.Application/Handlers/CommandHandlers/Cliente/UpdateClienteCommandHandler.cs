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
    public class UpdateClienteCommandHandler : IRequestHandler<UpdateClienteCommandRequest, ClienteDTO>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        public UpdateClienteCommandHandler(IClienteRepository repository, IMapper mapper)
        {
            _clienteRepository = repository;
            _mapper = mapper;
        }

        public async Task<ClienteDTO> Handle(UpdateClienteCommandRequest request, CancellationToken cancellationToken)
        {
            var validator = new UpdateClienteCommandValidator();
            if (validator.Validate(request).IsValid)
            {
                var clienteEntity = await _clienteRepository.GetByIdAsync(request.Id);

                if (clienteEntity == null)
                {
                    return null;
                }

                clienteEntity.SetName(request.Nombre);
                clienteEntity.SetTelefono(request.Telefono);
                Direccion direccion = new Direccion(request.Direccion.Calle, request.Direccion.Ciudad, request.Direccion.Provincia, request.Direccion.Pais, request.Direccion.CodigoPostal);
                clienteEntity.SetDireccion(direccion);

                _clienteRepository.Update(clienteEntity);
                await _clienteRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                ClienteDTO clienteDTO = _mapper.Map<ClienteDTO>(clienteEntity);

                return clienteDTO;
            }
            else
            {
                throw new InvalidModelException(ErrorParser.ParseFluentValidationErrorList(validator.Validate(request).Errors));
            }
        }
    }
}
