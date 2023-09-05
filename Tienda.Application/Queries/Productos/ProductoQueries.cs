using Dapper;
using MediatR;
using System.Data.SqlClient;
using Tienda.Application.Response;

namespace Tienda.Application.Queries
{
    public class ProductoQueries : IProductoQueries
    {
        private string _connectionString = string.Empty;

        public ProductoQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<IEnumerable<ProductoDTO>> GetProductosAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return await connection.QueryAsync<ProductoDTO>("SELECT * FROM Tienda.Productos");
            }
        }

        public async Task<IEnumerable<ProductoDTO>> GetProductoByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return await connection.QueryAsync<ProductoDTO>("SELECT * FROM Tienda.Productos WHERE Id=@id", new { id = id });
            }
        }
    }
}
