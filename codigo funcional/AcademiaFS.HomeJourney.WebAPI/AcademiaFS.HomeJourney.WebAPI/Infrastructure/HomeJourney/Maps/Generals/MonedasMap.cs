using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Maps.Generals
{
    public class MonedasMap : IEntityTypeConfiguration<Monedas>
    {
        public void Configure(EntityTypeBuilder<Monedas> builder)
        {
            builder.ToTable("Monedas");
            builder.HasKey(m => m.MonedaId);
            builder.Property(m => m.MonedaId)
                   .HasColumnName("Moneda_Id")
                   .IsRequired();
            builder.Property(m => m.Nombre)
                   .HasColumnName("Nombre")
                   .HasMaxLength(25);
            builder.Property(m => m.Simbolo)
                   .HasColumnName("Simbolo")
                   .HasMaxLength(1);
            builder.Property(m => m.ValorLempiras)
                   .HasColumnName("ValorLempiras")
                   .HasColumnType("smallmoney");
        }
    }
}
