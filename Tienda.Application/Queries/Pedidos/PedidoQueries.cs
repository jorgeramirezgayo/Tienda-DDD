using Dapper;
using MediatR;
using System.Data.SqlClient;
using Tienda.Application.Common.Enum;
using Tienda.Application.Models;
using Tienda.Application.Response;

namespace Tienda.Application.Queries
{
    public class PedidoQueries : IPedidoQueries
    {
        private string _connectionString = string.Empty;

        public PedidoQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }
        public async Task<IEnumerable<PedidoDTO>> GetPedidosAsync(PedidoQueryRequest request)
        {
            switch (request.QueryType)
            {
                case QueryType.Todos: return await GetPedidosAsync();
                case QueryType.Uno: return await GetPedidoByIdAsync(request.Id);
                case QueryType.ByIdProducto: return await GetPedidosByIdProductoAsync(request.ProductoId);
                default: throw new NotImplementedException();
            }
        }

        private async Task<IEnumerable<PedidoDTO>> GetPedidoByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var pedidos = await connection.QueryAsync<PedidoDTO>("SELECT * FROM Tienda.Pedidos WHERE Id=@id", new { id = id });

                if (pedidos != null && pedidos.Count() == 1)
                {
                    var lineas = await GetLineaPedidoAsync(id);

                    List<OrderItem> miLista = new List<OrderItem>();

                    foreach (var linea in lineas)
                    {
                        var item = new OrderItem();
                        item.Cantidad = linea.Cantidad;
                        item.ProductoId = linea.ProductoId;

                        miLista.Add(item);
                    }

                    pedidos.ToList().First().OrderItems = miLista;
                }
                return pedidos;
            }
        }

        private async Task<IEnumerable<PedidoDTO>> GetPedidosAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var pedidos = await connection.QueryAsync<PedidoDTO>("SELECT * FROM Tienda.Pedidos");

                if (pedidos != null)
                {
                    foreach (var pedido in pedidos)
                    {
                        List<OrderItem> miLista = new List<OrderItem>();

                        var lineas = await GetLineaPedidoAsync(pedido.Id);

                        foreach (var linea in lineas)
                        {
                            var item = new OrderItem();
                            item.Cantidad = linea.Cantidad;
                            item.ProductoId = linea.ProductoId;

                            miLista.Add(item);
                        }
                        pedido.OrderItems = miLista;
                    }
                }
                return pedidos;
            }
        }

        private async Task<IEnumerable<PedidoDTO>> GetPedidosByIdProductoAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var pedidos = await connection.QueryAsync<PedidoDTO>("SELECT p.* FROM Tienda.Pedidos AS p LEFT JOIN tienda.LineaPedidos AS l ON p.Id = l.PedidoId WHERE l.ProductoId =@id", new { id = id });

                if (pedidos != null)
                {
                    foreach (var pedido in pedidos)
                    {
                        List<OrderItem> miLista = new List<OrderItem>();

                        var lineas = await GetLineaPedidoAsync(pedido.Id);

                        foreach (var linea in lineas)
                        {
                            var item = new OrderItem();
                            item.Cantidad = linea.Cantidad;
                            item.ProductoId = linea.ProductoId;

                            miLista.Add(item);
                        }
                        pedido.OrderItems = miLista;
                    }
                }
                return pedidos;
            }
        }

        private async Task<IEnumerable<OrderItem>> GetLineaPedidoAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return await connection.QueryAsync<OrderItem>("SELECT * FROM Tienda.LineaPedidos WHERE PedidoId=@id", new { id = id });
            }
        }
    }
}
