using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tienda.Domain.AggregatesModel;
using Tienda.Infraestructure.Data;

namespace Tienda.Infraestructure.EntityConfigurations
{
    public class PedidoEntityTypeConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> pedidoConfiguration)
        {
            pedidoConfiguration.ToTable("Pedidos", TiendaDbContext.DEFAULT_SCHEMA);

            pedidoConfiguration.HasKey(c => c.Id);
            pedidoConfiguration.Ignore(c => c.DomainEvents);

            pedidoConfiguration
                .Property<int>("Id")
                .HasColumnOrder(1)
                .IsRequired();

            pedidoConfiguration
                .Property<DateTime>("Fecha")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("fecha")
                .HasColumnType("datetime")
                .HasColumnOrder(2)
                .IsRequired();

            pedidoConfiguration
                .Property<double>("Total")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("total")
                .HasColumnType("float")
                .HasColumnOrder(3)
                .IsRequired();

            pedidoConfiguration
                .Property<int>("_clienteId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("ClienteId")
                .HasColumnType("int")
                .HasColumnOrder(4)
                .IsRequired();

            // Relación a otra Entity de otro Aggregate
            pedidoConfiguration.HasOne<Cliente>()
                .WithMany()
                .HasForeignKey("_clienteId")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            // 0..N relationship to LineaPedido (child entity) in the Aggregate
            var navigation = pedidoConfiguration.Metadata.FindNavigation(nameof(Pedido.OrderItems));
            // DDD Patterns comment:
            //Set as field to access the LineaPedido collection property through its field
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}

