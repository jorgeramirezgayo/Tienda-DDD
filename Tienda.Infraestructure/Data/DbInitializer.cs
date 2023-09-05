using Microsoft.EntityFrameworkCore;
using Tienda.Domain.AggregatesModel;
using Tienda.Domain.SeedWork;

namespace Tienda.Domain.Infrastructure.Data
{
    public class DbInitializer
    {
        private readonly ModelBuilder _modelBuilder;

        public DbInitializer(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            // Initialize database: insert records in 'values list' tables (Enumeration classes)
            _modelBuilder.Entity<ClienteType>().HasData(Enumeration.GetAll<ClienteType>());
        }
    }
}
