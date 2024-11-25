using InvestmentOrders.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestmentOrders.Infrastructure.Data.Configurations;

public class OrdenConfiguration : IEntityTypeConfiguration<Orden>
{
    public void Configure(EntityTypeBuilder<Orden> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.NombreActivo).HasMaxLength(32).IsRequired();
        builder.Property(o => o.Cantidad).IsRequired();
        builder.Property(o => o.Precio).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(o => o.Operacion).IsRequired();
        builder.Property(o => o.MontoTotal).HasColumnType("decimal(18,2)");
        
        builder.HasOne(o => o.Activo)
            .WithMany()
            .HasForeignKey(o => o.ActivoId);
    }
}