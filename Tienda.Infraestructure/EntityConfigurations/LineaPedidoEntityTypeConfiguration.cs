using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tienda.Domain.AggregatesModel;
using Tienda.Infraestructure.Data;

namespace Tienda.Infraestructure.EntityConfigurations
{
    class LineaPedidoEntityTypeConfiguration : IEntityTypeConfiguration<LineaPedido>
    {
        public void Configure(EntityTypeBuilder<LineaPedido> LineaPedidoConfiguration)
        {
            LineaPedidoConfiguration.ToTable("LineaPedidos", TiendaDbContext.DEFAULT_SCHEMA);

            LineaPedidoConfiguration.HasKey(ci => ci.Id);
            LineaPedidoConfiguration.Ignore(ci => ci.DomainEvents);

            LineaPedidoConfiguration
                .Property<int>("Id")
                .HasColumnOrder(1)
                .IsRequired();

            LineaPedidoConfiguration
                .Property<int>("_productoId")
                .HasColumnName("ProductoId")
                .HasColumnOrder(2)
                .IsRequired();

            LineaPedidoConfiguration
                .Property<int>("_cantidad")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Cantidad")
                .HasColumnType("int")
                .HasColumnOrder(3)
                .IsRequired();

            // Relación a otra Entity de otro Aggregate
            LineaPedidoConfiguration.HasOne<Producto>()
                .WithMany()
                .HasForeignKey("_productoId")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            LineaPedidoConfiguration.Property<int>("PedidoId")
                .HasColumnOrder(4)
                .IsRequired();

        }
    }
}

