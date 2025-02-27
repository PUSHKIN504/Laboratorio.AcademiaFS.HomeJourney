using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Maps.Viaje
{
    public class ViajesdetallesMap : IEntityTypeConfiguration<Viajesdetalles>
    {
        public void Configure(EntityTypeBuilder<Viajesdetalles> builder)
        {
            builder.ToTable("Viajesdetalles");
            builder.HasKey(vd => vd.ViajedetalleId);
            builder.Property(vd => vd.ViajedetalleId)
                   .HasColumnName("Viajedetalle_id")
                   .IsRequired();
            builder.Property(vd => vd.ViajeId)
                   .HasColumnName("Viaje_id")
                   .IsRequired();
            builder.Property(vd => vd.ColaboradorId)
                   .HasColumnName("Colaborador_id")
                   .IsRequired();
            builder.Property(vd => vd.Distanciakilometros)
                   .HasColumnName("Distanciakilometros")
                   .HasColumnType("decimal(5,2)")
                   .IsRequired();
            builder.Property(vd => vd.Totalpagar)
                   .HasColumnName("Totalpagar")
                   .HasColumnType("decimal(10,2)")
                   .IsRequired();
            builder.Property(vd => vd.ColaboradorsucursalId)
                   .HasColumnName("Colaboradorsucursal_id")
                   .IsRequired();
            builder.Property(vd => vd.Activo)
                   .HasColumnName("Activo")
                   .IsRequired();
            builder.Property(vd => vd.Usuariocrea)
                   .HasColumnName("Usuariocrea")
                   .IsRequired();
            builder.Property(vd => vd.Fechacrea)
                   .HasColumnName("Fechacrea")
                   .HasColumnType("datetime")
                   .IsRequired();
            builder.Property(vd => vd.Usuariomodifica)
                   .HasColumnName("Usuariomodifica")
                   .IsRequired(false);
            builder.Property(vd => vd.Fechamodifica)
                   .HasColumnName("Fechamodifica")
                   .HasColumnType("datetime")
                   .IsRequired(false);
            builder.Property(vd => vd.MonedaId)
                   .HasColumnName("Moneda_Id")
                   .IsRequired(false);

            builder.HasOne(vd => vd.Viaje)
                   .WithMany(v => v.Viajesdetalles)
                   .HasForeignKey(vd => vd.ViajeId)
                   .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(vd => vd.Colaboradorsucursal)
                   .WithMany(cs => cs.Viajesdetalles)
                   .HasForeignKey(vd => vd.ColaboradorsucursalId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasOne(vd => vd.Moneda)
                   .WithMany(m => m.Viajesdetalles)
                   .HasForeignKey(vd => vd.MonedaId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
