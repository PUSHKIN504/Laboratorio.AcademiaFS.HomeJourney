using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Maps.Auth
{
    public class PantallasMap : IEntityTypeConfiguration<Pantallas>
    {
        public void Configure(EntityTypeBuilder<Pantallas> builder)
        {
            builder.ToTable("Pantallas");
            builder.HasKey(p => p.PantallaId);
            builder.Property(p => p.PantallaId)
                   .HasColumnName("Pantalla_id")
                   .IsRequired();
            builder.Property(p => p.Nombre)
                   .HasColumnName("Nombre")
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(p => p.Usuariocrea)
                   .HasColumnName("Usuariocrea")
                   .IsRequired();
            builder.Property(p => p.Fechacrea)
                   .HasColumnName("Fechacrea")
                   .HasColumnType("datetime")
                   .IsRequired();
            builder.Property(p => p.Usuariomodifica)
                   .HasColumnName("Usuariomodifica")
                   .IsRequired(false);
            builder.Property(p => p.Fechamodifica)
                   .HasColumnName("Fechamodifica")
                   .HasColumnType("datetime")
                   .IsRequired(false);
            builder.Property(p => p.Activo)
                   .HasColumnName("Activo")
                   .IsRequired();

            // Si deseas tener relaciones de auditoría a Usuarios, configúralas aquí:
            builder.HasOne<Usuarios>()
                   .WithMany()
                   .HasForeignKey(p => p.Usuariocrea)
                   .OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasOne<Usuarios>()
                   .WithMany()
                   .HasForeignKey(p => p.Usuariomodifica)
                   .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
