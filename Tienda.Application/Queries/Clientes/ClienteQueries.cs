using Dapper;
using System.Data.SqlClient;
using Tienda.Application.Common.Enum;
using Tienda.Application.Response;

namespace Tienda.Application.Queries
{
    public class ClienteQueries : IClienteQueries
    {
        private string _connectionString = string.Empty;

        public ClienteQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }
        public async Task<IEnumerable<ClienteDTO>> GetClientesAsync(ClienteQueryRequest request)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                switch (request.QueryType)
                {
                    case QueryType.Todos: return await connection.QueryAsync<ClienteDTO>("SELECT * FROM Tienda.Clientes");
                    case QueryType.Uno: return await connection.QueryAsync<ClienteDTO>("SELECT * FROM Tienda.Clientes WHERE Id=@id", new { id = request.Id });
                    default: throw new NotImplementedException();
                }
            }
        }
    }
}
