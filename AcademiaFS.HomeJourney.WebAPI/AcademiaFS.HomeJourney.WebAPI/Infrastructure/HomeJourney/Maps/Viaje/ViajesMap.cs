using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Maps.Viaje
{
    public class ViajesMap : IEntityTypeConfiguration<Viajes>
    {
        public void Configure(EntityTypeBuilder<Viajes> builder)
        {
            builder.ToTable("Viajes");
            builder.HasKey(v => v.ViajeId);
            builder.Property(v => v.ViajeId)
                   .HasColumnName("Viaje_id")
                   .IsRequired();
            builder.Property(v => v.SucursalId)
                   .HasColumnName("Sucursal_id")
                   .IsRequired();
            builder.Property(v => v.TransportistaId)
                   .HasColumnName("Transportista_id")
                   .IsRequired();
            builder.Property(v => v.EstadoId)
                   .HasColumnName("Estado_id")
                   .IsRequired();
            builder.Property(v => v.Viajehora)
                   .HasColumnName("Viajehora")
                   .HasColumnType("time(7)")
                   .IsRequired();
            builder.Property(v => v.Viajefecha)
                   .HasColumnName("Viajefecha")
                   .HasColumnType("date")
                   .IsRequired();
            builder.Property(v => v.Totalkilometros)
                   .HasColumnName("Totalkilometros")
                   .HasColumnType("decimal(5,2)")
                   .IsRequired();
            builder.Property(v => v.Totalpagar)
                   .HasColumnName("Totalpagar")
                   .HasColumnType("decimal(10,2)")
                   .IsRequired();
            builder.Property(v => v.Activo)
                   .HasColumnName("Activo")
                   .IsRequired();
            builder.Property(v => v.Usuariocrea)
                   .HasColumnName("Usuariocrea")
                   .IsRequired();
            builder.Property(v => v.Fechacrea)
                   .HasColumnName("Fechacrea")
                   .HasColumnType("datetime")
                   .IsRequired();
            builder.Property(v => v.Usuariomodifica)
                   .HasColumnName("Usuariomodifica")
                   .IsRequired(false);
            builder.Property(v => v.Fechamodifica)
                   .HasColumnName("Fechamodifica")
                   .HasColumnType("datetime")
                   .IsRequired(false);
            builder.Property(v => v.MonedaId)
                   .HasColumnName("Moneda_Id")
                   .IsRequired(false);

            builder.HasOne(v => v.Sucursal)
                   .WithMany(s => s.Viajes)
                   .HasForeignKey(v => v.SucursalId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasOne(v => v.Transportista)
                   .WithMany(t => t.Viajes)
                   .HasForeignKey(v => v.TransportistaId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasOne(v => v.Estado)
                   .WithMany(e => e.Viajes)
                   .HasForeignKey(v => v.EstadoId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasOne(v => v.Moneda)
                   .WithMany(m => m.Viajes)
                   .HasForeignKey(v => v.MonedaId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(v => v.Solicitudesviajes)
                   .WithOne(sv => sv.Viaje)
                   .HasForeignKey(sv => sv.ViajeId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasMany(v => v.Valoracionesviajes)
                   .WithOne(rv => rv.Viaje)
                   .HasForeignKey(rv => rv.ViajeId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasMany(v => v.Viajesdetalles)
                   .WithOne(vd => vd.Viaje)
                   .HasForeignKey(vd => vd.ViajeId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
