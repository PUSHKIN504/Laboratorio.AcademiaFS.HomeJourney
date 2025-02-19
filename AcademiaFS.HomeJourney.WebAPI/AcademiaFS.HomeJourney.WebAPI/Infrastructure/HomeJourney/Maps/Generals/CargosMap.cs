using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Maps.Generals
{
    public class CargosMap : IEntityTypeConfiguration<Cargos>
    {
        public void Configure(EntityTypeBuilder<Cargos> builder)
        {
            builder.ToTable("Cargos");
            builder.HasKey(x => x.CargoId);
            builder.Property(x => x.CargoId)
                   .HasColumnName("Cargo_id")
                   .IsRequired();
            builder.Property(x => x.Nombre)
                   .HasColumnName("Nombre")
                   .HasMaxLength(25);
        }
    }
}
