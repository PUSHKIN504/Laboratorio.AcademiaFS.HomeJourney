using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Maps.Generals
{
    public class CiudadesMap : IEntityTypeConfiguration<Ciudades>
    {
        public void Configure(EntityTypeBuilder<Ciudades> builder)
        {
            builder.ToTable("Ciudades");
            builder.HasKey(c => c.CiudadId);
            builder.Property(c => c.CiudadId)
                   .HasColumnName("Ciudad_id")
                   .IsRequired();
            builder.Property(c => c.Nombre)
                   .HasColumnName("Nombre")
                   .HasMaxLength(75)
                   .IsRequired();
            builder.Property(c => c.DepartamentoId)
                   .HasColumnName("Departamento_id")
                   .IsRequired();
            builder.Property(c => c.Activo)
                   .HasColumnName("Activo")
                   .IsRequired();

            builder.HasOne(c => c.Departamento)
                   .WithMany(d => d.Ciudades)
                   .HasForeignKey(c => c.DepartamentoId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
