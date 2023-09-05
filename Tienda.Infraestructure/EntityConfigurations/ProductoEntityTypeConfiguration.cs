using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tienda.Domain.AggregatesModel;
using Tienda.Infraestructure.Data;

namespace Tienda.Infraestructure.EntityConfigurations
{
    public class ProductoEntityTypeConfiguration : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> productoConfiguration)
        {
            productoConfiguration.ToTable("Productos", TiendaDbContext.DEFAULT_SCHEMA);

            productoConfiguration.HasKey(c => c.Id);
            productoConfiguration.Ignore(c => c.DomainEvents);

            productoConfiguration
                .Property<int>("Id")
                .HasColumnOrder(1)
                .IsRequired();

            productoConfiguration
                .Property<string>("Nombre")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("nombre")
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .HasColumnOrder(3)
                .IsRequired();

            productoConfiguration
                .Property<double>("Precio")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("precio")
                .HasColumnType("float")
                .HasColumnOrder(2)
                .IsRequired();
        }
    }
}
