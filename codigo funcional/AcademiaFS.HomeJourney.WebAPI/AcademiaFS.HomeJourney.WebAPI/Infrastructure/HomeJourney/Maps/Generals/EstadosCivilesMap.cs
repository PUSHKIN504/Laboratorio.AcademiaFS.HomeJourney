using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Maps.Generals
{
    public class EstadoscivilesMap : IEntityTypeConfiguration<Estadosciviles>
    {
        public void Configure(EntityTypeBuilder<Estadosciviles> builder)
        {
            builder.ToTable("Estadosciviles");
            builder.HasKey(e => e.EstadocivilId);
            builder.Property(e => e.EstadocivilId)
                   .HasColumnName("Estadocivil_id")
                   .IsRequired();
            builder.Property(e => e.Nombre)
                   .HasColumnName("Nombre")
                   .HasMaxLength(45)
                   .IsRequired();
        }
    }
}
