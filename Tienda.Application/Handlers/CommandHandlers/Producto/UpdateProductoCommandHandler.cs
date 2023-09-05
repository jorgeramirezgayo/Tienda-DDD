using AutoMapper;
using MediatR;
using Tienda.Application.Commands;
using Tienda.Application.Exceptions;
using Tienda.Application.Mappers;
using Tienda.Application.Response;
using Tienda.Application.Validators;
using Tienda.Domain.AggregatesModel;
using Tienda.Domain.Events;

namespace Tienda.Application.Handlers
{
    public class UpdateProductoCommandHandler : IRequestHandler<UpdateProductoCommandRequest, ProductoDTO>
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UpdateProductoCommandHandler(IProductoRepository repository, IMediator mediator, IMapper mapper)
        {
            _productoRepository = repository;
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<ProductoDTO> Handle(UpdateProductoCommandRequest request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProductoCommandValidator();
            if (validator.Validate(request).IsValid)
            {
                var productoEntity = await _productoRepository.GetByIdAsync(request.Id);

                if (productoEntity == null)
                {
                    return null;
                }
                productoEntity.SetName(request.Nombre);
                productoEntity.SetPrecio(request.Precio);

                productoEntity.AddDomainEvent(new ChangePriceWhenUpdateProductoDomainEvent(request.Id));

                _productoRepository.Update(productoEntity);
                await _productoRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                ProductoDTO productoDTO = _mapper.Map<ProductoDTO>(productoEntity);

                return productoDTO;
            }
            else
            {
                throw new InvalidModelException(ErrorParser.ParseFluentValidationErrorList(validator.Validate(request).Errors));
            }
        }
    }
}
