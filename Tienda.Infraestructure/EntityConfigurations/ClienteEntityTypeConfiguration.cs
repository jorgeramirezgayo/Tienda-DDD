using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tienda.Domain.AggregatesModel;
using Tienda.Infraestructure.Data;

namespace Tienda.Infraestructure.EntityConfigurations
{
    public class ClienteEntityTypeConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> clienteConfiguration)
        {
            clienteConfiguration.ToTable("Clientes", TiendaDbContext.DEFAULT_SCHEMA);

            clienteConfiguration.HasKey(c => c.Id);
            clienteConfiguration.Ignore(c => c.DomainEvents);

            clienteConfiguration
                .Property<int>("Id")
                .HasColumnOrder(1)
                .IsRequired();

            clienteConfiguration
                .Property<string>("Nombre")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("nombre")
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .HasColumnOrder(2)
                .IsRequired();

            clienteConfiguration
                .Property<string>("Telefono")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("telefono")
                .HasColumnType("varchar")
                .HasMaxLength(15)
                .HasColumnOrder(3)
                .IsRequired();

            clienteConfiguration
                .OwnsOne(c => c.Direccion, vo =>
                {
                    vo.WithOwner();
                    vo.Property(a => a.Calle).HasColumnType("varchar").HasColumnOrder(4).HasMaxLength(100);
                    vo.Property(a => a.Ciudad).HasColumnType("varchar").HasColumnOrder(5).HasMaxLength(100);
                    vo.Property(a => a.Provincia).HasColumnType("varchar").HasColumnOrder(6).HasMaxLength(50);
                    vo.Property(a => a.Pais).HasColumnType("varchar").HasColumnOrder(7).HasMaxLength(35);
                    vo.Property(a => a.CodigoPostal).HasColumnType("varchar").HasColumnOrder(8).HasMaxLength(5);
                });

            // FK to ClienteTypes (Enumeration class)
            clienteConfiguration
                .Property<int>("_clienteTypeId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("clienteType")
                .HasColumnType("int")
                .HasColumnOrder(9)
                .IsRequired();

            // Relationship to ClienteTypes (Enumeration class) in the Aggregate
            clienteConfiguration.HasOne(t => t.ClienteType)
                .WithMany()
                .HasForeignKey("_clienteTypeId")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
