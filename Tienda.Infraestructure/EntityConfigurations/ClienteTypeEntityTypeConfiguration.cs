using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tienda.Domain.AggregatesModel;
using Tienda.Infraestructure.Data;

namespace Tienda.Infraestructure.EntityConfigurations
{
    public class ClienteTypeEntityTypeConfiguration : IEntityTypeConfiguration<ClienteType>
    {
        public void Configure(EntityTypeBuilder<ClienteType> customerTypesConfiguration)
        {
            customerTypesConfiguration.ToTable("ClienteTypes", TiendaDbContext.DEFAULT_SCHEMA);

            customerTypesConfiguration.HasKey(ct => ct.Id);

            customerTypesConfiguration.Property(ct => ct.Id)
                // .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            customerTypesConfiguration.Property(ct => ct.Name)
                .HasMaxLength(50)
                .HasColumnType("varchar")
                .IsRequired();
        }
    }
}
