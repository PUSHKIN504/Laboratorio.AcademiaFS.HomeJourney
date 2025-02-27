using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Maps.Auth
{
    public class PantallasrolesMap : IEntityTypeConfiguration<Pantallasroles>
    {
        public void Configure(EntityTypeBuilder<Pantallasroles> builder)
        {
            builder.ToTable("Pantallasroles");
            builder.HasKey(pr => pr.PantallarolId);
            builder.Property(pr => pr.PantallarolId)
                   .HasColumnName("Pantallarol_id")
                   .IsRequired();
            builder.Property(pr => pr.Fechacrea)
                   .HasColumnName("Fechacrea")
                   .HasColumnType("datetime")
                   .IsRequired();
            builder.Property(pr => pr.PantallaId)
                   .HasColumnName("Pantalla_id")
                   .IsRequired();
            builder.Property(pr => pr.RolId)
                   .HasColumnName("Rol_id")
                   .IsRequired();

            builder.HasOne(pr => pr.Pantalla)
                   .WithMany(p => p.Pantallasroles)
                   .HasForeignKey(pr => pr.PantallaId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasOne(pr => pr.Rol)
                   .WithMany(r => r.Pantallasroles)
                   .HasForeignKey(pr => pr.RolId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
