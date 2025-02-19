using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Maps.Generals
{
    public class ColaboradoressucursalesMap : IEntityTypeConfiguration<Colaboradoressucursales>
    {
        public void Configure(EntityTypeBuilder<Colaboradoressucursales> builder)
        {
            builder.ToTable("Colaboradoressucursales");
            builder.HasKey(x => x.ColaboradorsucursalId);
            builder.Property(x => x.ColaboradorsucursalId)
                   .HasColumnName("Colaboradorsucursal_id")
                   .IsRequired();
            builder.Property(x => x.ColaboradorId)
                   .HasColumnName("Colaborador_id")
                   .IsRequired();
            builder.Property(x => x.SucursalId)
                   .HasColumnName("Sucursal_id")
                   .IsRequired();
            builder.Property(x => x.Distanciakilometro)
                   .HasColumnName("Distanciakilometro")
                   .HasColumnType("decimal(5,2)")
                   .IsRequired();
            builder.Property(x => x.Activo)
                   .HasColumnName("Activo")
                   .IsRequired();
            builder.Property(x => x.Usuariocrea)
                   .HasColumnName("Usuariocrea")
                   .IsRequired();
            builder.Property(x => x.Fechacrea)
                   .HasColumnName("Fechacrea")
                   .HasColumnType("datetime")
                   .IsRequired();
            builder.Property(x => x.Usuariomodifica)
                   .HasColumnName("Usuariomodifica")
                   .IsRequired(false);
            builder.Property(x => x.Fechamodifica)
                   .HasColumnName("Fechamodifica")
                   .HasColumnType("datetime")
                   .IsRequired(false);

            builder.HasOne(x => x.Colaborador)
                   .WithMany(c => c.Colaboradoressucursales)
                   .HasForeignKey(x => x.ColaboradorId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Sucursal)
                   .WithMany(s => s.Colaboradoressucursales)
                   .HasForeignKey(x => x.SucursalId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Configurar relaciones de auditoría:
            builder.HasOne<Usuarios>()
                   .WithMany()
                   .HasForeignKey(x => x.Usuariocrea)
                   .OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasOne<Usuarios>()
                   .WithMany()
                   .HasForeignKey(x => x.Usuariomodifica)
                   .OnDelete(DeleteBehavior.ClientSetNull);

            //builder.hasone(vd => vd.colaboradorsucursal)
            //       .withmany(c => c.viajesdetalles)
            //       .hasforeignkey(vd => vd.colaboradorsucursalId)
            //       .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
