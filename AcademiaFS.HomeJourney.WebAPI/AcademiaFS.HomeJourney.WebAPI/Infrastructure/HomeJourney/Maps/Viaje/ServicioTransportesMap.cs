using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Maps.Viaje
{
    public class ServiciostransportesMap : IEntityTypeConfiguration<Serviciostransportes>
    {
        public void Configure(EntityTypeBuilder<Serviciostransportes> builder)
        {
            builder.ToTable("Serviciostransporte");
            builder.HasKey(s => s.ServiciotransporteId);
            builder.Property(s => s.ServiciotransporteId)
                   .HasColumnName("Serviciotransporte_id")
                   .IsRequired();
            builder.Property(s => s.Nombre)
                   .HasColumnName("Nombre")
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(s => s.Descripcion)
                   .HasColumnName("Descripcion")
                   .HasMaxLength(150)
                   .IsRequired();
            builder.Property(s => s.Email)
                   .HasColumnName("Email")
                   .HasMaxLength(100)
                   .IsRequired();
            builder.Property(s => s.Usuariocrea)
                   .HasColumnName("Usuariocrea")
                   .IsRequired();
            builder.Property(s => s.Fechacrea)
                   .HasColumnName("Fechacrea")
                   .HasColumnType("datetime")
                   .IsRequired();
            builder.Property(s => s.Usuariomodifica)
                   .HasColumnName("Usuariomodifica")
                   .IsRequired(false);
            builder.Property(s => s.Fechamodifica)
                   .HasColumnName("Fechamodifica")
                   .HasColumnType("datetime")
                   .IsRequired(false);
            builder.Property(s => s.Activo)
                   .HasColumnName("Activo")
                   .IsRequired();

            builder.HasIndex(s => s.Email)
                   .IsUnique();
        }
    }
}
