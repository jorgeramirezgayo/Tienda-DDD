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
    public class CreateProductoCommandHandler : IRequestHandler<CreateProductoCommandRequest, ProductoDTO>
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;
        public CreateProductoCommandHandler(IProductoRepository repository, IMapper mapper)
        {
            _productoRepository = repository;
            _mapper = mapper;
        }

        public async Task<ProductoDTO> Handle(CreateProductoCommandRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductoCommandValidator();
            if (validator.Validate(request).IsValid)
            {
                Producto productoEntity = new Producto(request.Nombre, request.Precio);

                _productoRepository.Add(productoEntity);
                await _productoRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                ProductoDTO productoDTO = _mapper.Map<ProductoDTO>(productoEntity);

                return productoDTO;
            } else
            {
                throw new InvalidModelException(ErrorParser.ParseFluentValidationErrorList(validator.Validate(request).Errors));
            }
        }
    }
}
