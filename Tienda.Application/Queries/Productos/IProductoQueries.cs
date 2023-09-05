using Tienda.Application.Response;

namespace Tienda.Application.Queries
{
    public interface IProductoQueries
    {
        Task<IEnumerable<ProductoDTO>> GetProductosAsync();
        Task<IEnumerable<ProductoDTO>> GetProductoByIdAsync(int id);
    }
}
