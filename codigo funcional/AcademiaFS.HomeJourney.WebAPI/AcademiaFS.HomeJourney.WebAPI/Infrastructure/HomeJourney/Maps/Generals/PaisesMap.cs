using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Maps.Generals
{
    public class PaisesMap : IEntityTypeConfiguration<Paises>
    {
        public void Configure(EntityTypeBuilder<Paises> builder)
        {
            builder.ToTable("Paises");
            builder.HasKey(p => p.PaisId);
            builder.Property(p => p.PaisId)
                   .HasColumnName("Pais_id")
                   .IsRequired();
            builder.Property(p => p.Nombre)
                   .HasColumnName("Nombre")
                   .HasMaxLength(50);
            builder.Property(p => p.Activo)
                   .HasColumnName("Activo")
                   .HasDefaultValue(true);
        }
    }
}
