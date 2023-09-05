using Microsoft.EntityFrameworkCore;
using Tienda.Domain.AggregatesModel;
using Tienda.Domain.SeedWork;
using Tienda.Infraestructure.Data;

namespace Tienda.Infraestructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly TiendaDbContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public ClienteRepository(TiendaDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Cliente Add(Cliente entity)
        {
            if (entity.IsTransient())
                return _context.Cliente.Add(entity).Entity;
            else
                return entity;
        }

        public void Delete(Cliente entity)
        {
            _context.Cliente.Remove(entity);
        }

        public async Task<Cliente> GetByIdAsync(int id)
        {
            var cliente = await _context
                                .Cliente
                                .FirstOrDefaultAsync(a => a.Id == id);

            if (cliente != null)
            {
                await _context.Entry(cliente).Reference(r => r.ClienteType).LoadAsync();   // Enumeration class
            }
            return cliente;
        }

        public void Update(Cliente entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
