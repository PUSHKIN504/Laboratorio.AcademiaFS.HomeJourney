using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Maps.Auth
{
    public class UsuariosMap : IEntityTypeConfiguration<Usuarios>
    {
        public void Configure(EntityTypeBuilder<Usuarios> builder)
        {
            builder.ToTable("Usuarios");
            builder.HasKey(u => u.UsuarioId);
            builder.Property(u => u.UsuarioId)
                   .HasColumnName("Usuario_id")
                   .IsRequired();
            builder.Property(u => u.Username)
                   .HasColumnName("Username")
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(u => u.Passwordhash)
                   .HasColumnName("Passwordhash")
                   .IsRequired();
            builder.Property(u => u.ColaboradorId)
                   .HasColumnName("Colaborador_id")
                   .IsRequired();
            builder.Property(u => u.Esadmin)
                   .HasColumnName("Esadmin")
                   .IsRequired();
            builder.Property(u => u.Activo)
                   .HasColumnName("Activo")
                   .IsRequired();
            //aqui esta el error
            builder.HasOne(u => u.Colaborador)
                   .WithMany(p => p.Usuarios)
                   .HasForeignKey(u => u.ColaboradorId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
