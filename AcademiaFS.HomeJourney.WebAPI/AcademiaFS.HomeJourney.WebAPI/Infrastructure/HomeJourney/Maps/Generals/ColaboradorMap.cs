using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Maps.Generals
{
    public class ColaboradoresMap : IEntityTypeConfiguration<Colaboradores>
    {
        public void Configure(EntityTypeBuilder<Colaboradores> builder)
        {
            builder.ToTable("Colaboradores");
            builder.HasKey(x => x.ColaboradorId);
            builder.Property(x => x.ColaboradorId)
                   .HasColumnName("Colaborador_id")
                   .IsRequired();
            builder.Property(x => x.PersonaId)
                   .HasColumnName("Persona_id")
                   .IsRequired();
            builder.Property(x => x.RolId)
                   .HasColumnName("Rol_id")
                   .IsRequired();
            builder.Property(x => x.CargoId)
                   .HasColumnName("Cargo_id")
                   .IsRequired();
            builder.Property(x => x.Direccion)
                   .HasColumnName("Direccion")
                   .HasMaxLength(150)
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
            builder.Property(x => x.Latitud)
                   .HasColumnName("Latitud")
                   .HasColumnType("decimal(9,8)")
                   .IsRequired();
            builder.Property(x => x.Longitud)
                   .HasColumnName("Longitud")
                   .HasColumnType("decimal(9,8)")
                   .IsRequired();

            builder.HasOne(x => x.Cargo)
                   .WithMany(c => c.Colaboradores)
                   .HasForeignKey(x => x.CargoId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Persona)
                   .WithMany(p => p.Colaborador)
                   .HasForeignKey(x => x.PersonaId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Rol)
                   .WithMany(r => r.Colaboradores)
                   .HasForeignKey(x => x.RolId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
