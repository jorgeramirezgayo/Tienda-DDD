using Microsoft.EntityFrameworkCore;
using Tienda.Domain.AggregatesModel;
using Tienda.Domain.SeedWork;
using Tienda.Infraestructure.Data;

namespace Tienda.Infraestructure.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly TiendaDbContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public ProductoRepository(TiendaDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Producto Add(Producto entity)
        {
            if (entity.IsTransient())
                return _context.Producto.Add(entity).Entity;
            else
                return entity;
        }

        public void Delete(Producto entity)
        {
            _context.Producto.Remove(entity);
        }

        public async Task<Producto> GetByIdAsync(int id)
        {
            var producto = await _context
                                .Producto
                                .FirstOrDefaultAsync(a => a.Id == id);
            return producto;
        }

        public void Update(Producto entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
