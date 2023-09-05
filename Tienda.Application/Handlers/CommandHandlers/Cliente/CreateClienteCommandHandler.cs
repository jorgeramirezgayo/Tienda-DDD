﻿using AutoMapper;
using MediatR;
using Tienda.Application.Commands;
using Tienda.Application.Exceptions;
using Tienda.Application.Mappers;
using Tienda.Application.Response;
using Tienda.Application.Validators;
using Tienda.Domain.AggregatesModel;

namespace Tienda.Application.Handlers
{
    public class CreateClienteCommandHandler : IRequestHandler<CreateClienteCommandRequest, ClienteDTO>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        public CreateClienteCommandHandler(IClienteRepository repository, IMapper mapper)
        {
            _clienteRepository = repository;
            _mapper = mapper;
        }
        public async Task<ClienteDTO> Handle(CreateClienteCommandRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateClienteCommandValidator();
            if (validator.Validate(request).IsValid)
            {
                Direccion direccion = new Direccion(request.Direccion.Calle, request.Direccion.Ciudad, request.Direccion.Provincia, request.Direccion.Pais, request.Direccion.CodigoPostal);
                Cliente clienteEntity = new Cliente(request.Nombre, request.Telefono, direccion);

                _clienteRepository.Add(clienteEntity);
                await _clienteRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                ClienteDTO clienteDTO = _mapper.Map<ClienteDTO>(clienteEntity);

                return clienteDTO;

            } else
            {
                throw new InvalidModelException(ErrorParser.ParseFluentValidationErrorList(validator.Validate(request).Errors));
            }
        }
    }
}
