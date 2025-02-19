using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Maps.Generals
{
    public class EstadosMap : IEntityTypeConfiguration<Estados>
    {
        public void Configure(EntityTypeBuilder<Estados> builder)
        {
            builder.ToTable("Estados");
            builder.HasKey(e => e.EstadoId);
            builder.Property(e => e.EstadoId)
                   .HasColumnName("Estado_id")
                   .IsRequired();
            builder.Property(e => e.Nombre)
                   .HasColumnName("Nombre")
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(e => e.Descripcion)
                   .HasColumnName("Descripcion")
                   .HasMaxLength(255)
                   .IsRequired();
        }
    }
}
