using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Maps.Viaje
{
    public class SolicitudesviajesMap : IEntityTypeConfiguration<Solicitudesviajes>
    {
        public void Configure(EntityTypeBuilder<Solicitudesviajes> builder)
        {
            builder.ToTable("Solicitudesviajes");
            builder.HasKey(s => s.SolicitudviajeId);
            builder.Property(s => s.SolicitudviajeId)
                   .HasColumnName("Solicitudviaje_id")
                   .IsRequired();
            builder.Property(s => s.ColaboradorId)
                   .HasColumnName("Colaborador_id")
                   .IsRequired();
            builder.Property(s => s.Fechasolicitud)
                   .HasColumnName("Fechasolicitud")
                   .HasColumnType("date")
                   .IsRequired();
            builder.Property(s => s.ViajeId)
                   .HasColumnName("Viaje_id")
                   .IsRequired();
            builder.Property(s => s.EstadoId)
                   .HasColumnName("Estado_id")
                   .IsRequired();
            builder.Property(s => s.Comentarios)
                   .HasColumnName("Comentarios")
                   .HasMaxLength(150);
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

            builder.HasOne(s => s.Colaborador)
                   .WithMany(c => c.Solicitudesviajes)
                   .HasForeignKey(s => s.ColaboradorId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasOne(s => s.Estado)
                   .WithMany(e => e.Solicitudesviajes)
                   .HasForeignKey(s => s.EstadoId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasOne(s => s.Viaje)
                   .WithMany(v => v.Solicitudesviajes)
                   .HasForeignKey(s => s.ViajeId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
