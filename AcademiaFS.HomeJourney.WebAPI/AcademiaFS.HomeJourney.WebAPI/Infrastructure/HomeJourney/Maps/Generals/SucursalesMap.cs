using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Maps.Generals
{
    public class SucursalesMap : IEntityTypeConfiguration<Sucursales>
    {
        public void Configure(EntityTypeBuilder<Sucursales> builder)
        {
            builder.ToTable("Sucursales");
            builder.HasKey(s => s.SucursalId);
            builder.Property(s => s.SucursalId)
                   .HasColumnName("Sucursal_id")
                   .IsRequired();
            builder.Property(s => s.Nombre)
                   .HasColumnName("Nombre")
                   .HasMaxLength(100)
                   .IsRequired();
            builder.Property(s => s.Direccion)
                   .HasColumnName("Direccion")
                   .HasMaxLength(100)
                   .IsRequired();
            builder.Property(s => s.Activo)
                   .HasColumnName("Activo")
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
            builder.Property(s => s.Latitud)
                   .HasColumnName("Latitud")
                   .HasColumnType("decimal(9,8)")
                   .IsRequired();
            builder.Property(s => s.Longitud)
                   .HasColumnName("Longitud")
                   .HasColumnType("decimal(9,8)")
                   .IsRequired();
            builder.Property(s => s.JefeId) 
                   .HasColumnName("Jefe_id")
                   .IsRequired(false);

            // Relaciones de auditoría
            builder.HasOne<Usuarios>()
                   .WithMany()
                   .HasForeignKey(s => s.Usuariocrea)
                   .OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasOne<Usuarios>()
                   .WithMany()
                   .HasForeignKey(s => s.Usuariomodifica)
                   .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
