using AutoMapper;
using Tienda.Application.Models;
using Tienda.Domain.AggregatesModel;

namespace Tienda.Application.Mappers
{
    public class LineaPedidoToOrderItemValueConverter : IValueConverter<List<LineaPedido>, List<OrderItem>>
    {
        public List<OrderItem> Convert(List<LineaPedido> source, ResolutionContext context)
        {
            List<OrderItem> result = new List<OrderItem>();

            foreach (var item in source)
            {
                result.Add(new OrderItem()
                {
                    Cantidad = item.Cantidad,
                    ProductoId = item.ProductoId
                });
            }

            return result;
        }
    }
}