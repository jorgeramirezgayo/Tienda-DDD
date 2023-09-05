using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Tienda.Domain.AggregatesModel;
using Tienda.Domain.Infrastructure.Data;
using Tienda.Domain.SeedWork;
using Tienda.Infraestructure.EntityConfigurations;
using Tienda.Infraestructure.Extensions;

namespace Tienda.Infraestructure.Data
{
    public class TiendaDbContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "tienda";
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<ClienteType> ClienteType { get; set; }
        public DbSet<LineaPedido> LineaPedido { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Producto> Producto { get; set; }
        private readonly IMediator _mediator;

        public TiendaDbContext(DbContextOptions<TiendaDbContext> options) : base(options) { }

        public TiendaDbContext(DbContextOptions<TiendaDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ClienteTypeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LineaPedidoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PedidoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductoEntityTypeConfiguration());

            var allEntities = modelBuilder.Model.GetEntityTypes();
            foreach (var entity in allEntities)
            {
                var type = entity.ClrType;
                if (!type.IsSubclassOf(typeof(Enumeration)) && !type.IsSubclassOf(typeof(ValueObject)))
                {
                    entity.AddProperty("Created", typeof(DateTime));
                    entity.AddProperty("Updated", typeof(DateTime));
                }
            }

            new DbInitializer(modelBuilder).Seed();
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(this);

            CustomizeDbContextChanges();

            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        private void CustomizeDbContextChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                // Never update 'values list' tables (Enumeration classes) in the database
                if (entry.Entity is Enumeration)
                    entry.State = EntityState.Unchanged;

                // Shadow Properties: set values (exclude Enumeration and ValueObject classes)
                if (!(entry.Entity is Enumeration) && !(entry.Entity is ValueObject))
                {
                    // Note: if you need, you can exclude by using entity name (table name). Example:
                    //      Enumeration class: if (!entry.Metadata.Name.Contains("Contact"))

                    var timestamp = DateTime.Now;
                    entry.Property("Updated").CurrentValue = timestamp;
                    if (entry.State == EntityState.Added)
                    {
                        entry.Property("Created").CurrentValue = timestamp;
                    }
                }
            }
        }
        public class TemplateDbContextFactory : IDesignTimeDbContextFactory<TiendaDbContext>
        {
            public TiendaDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<TiendaDbContext>();
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Initial Catalog=Shop;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");
                return new TiendaDbContext(optionsBuilder.Options);
            }
        }
    }
}
