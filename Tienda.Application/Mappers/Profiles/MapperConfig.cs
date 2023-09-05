using AutoMapper;
using Tienda.Application.Response;
using Tienda.Domain.AggregatesModel;

namespace Tienda.Application.Mappers
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Cliente, ClienteDTO>()
                .ForMember(dto => dto.ClienteType, opt => opt.MapFrom(src => src.ClienteType.Id));
            CreateMap<Pedido, PedidoDTO>()
                .ForMember(dto => dto.OrderItems, opt => opt.ConvertUsing<LineaPedidoToOrderItemValueConverter, List<LineaPedido>>());
            CreateMap<Producto, ProductoDTO>();
        }
    }
}
