using Microsoft.EntityFrameworkCore;
using Tienda.Domain.AggregatesModel;
using Tienda.Domain.SeedWork;
using Tienda.Infraestructure.Data;

namespace Tienda.Infraestructure.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly TiendaDbContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public PedidoRepository(TiendaDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Pedido Add(Pedido entity)
        {
            if (entity.IsTransient())
                return _context.Pedido.Add(entity).Entity;
            else
                return entity;
        }

        public void Delete(Pedido entity)
        {
            _context.Pedido.Remove(entity);
        }

        public async Task<Pedido> GetByIdAsync(int id)
        {
            var pedido = await _context
                                .Pedido
                                .FirstOrDefaultAsync(a => a.Id == id);

            if (pedido == null)
            {
                pedido = _context
                            .Pedido
                            .Local
                            .FirstOrDefault(a => a.Id == id);
            }

            if (pedido != null)
            {
                await _context.Entry(pedido).Collection(c => c.OrderItems).LoadAsync(); // Child entities
/*                await _context.Entry(pedido).Reference(r => r.OrderItems).LoadAsync();*/   // Enumeration class
            }

            return pedido;
        }

        public void Update(Pedido entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
