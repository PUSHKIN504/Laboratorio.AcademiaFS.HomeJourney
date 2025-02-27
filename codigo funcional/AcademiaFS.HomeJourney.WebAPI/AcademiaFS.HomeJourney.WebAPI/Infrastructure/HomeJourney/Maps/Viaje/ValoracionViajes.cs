using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Maps.Viaje
{
    public class ValoracionesviajesMap : IEntityTypeConfiguration<Valoracionesviajes>
    {
        public void Configure(EntityTypeBuilder<Valoracionesviajes> builder)
        {
            builder.ToTable("Valoracionesviajes");
            builder.HasKey(v => v.ValoracionviajeId);
            builder.Property(v => v.ValoracionviajeId)
                   .HasColumnName("Valoracionviaje_id")
                   .IsRequired();
            builder.Property(v => v.Valoracionnota)
                   .HasColumnName("Valoracionnota")
                   .IsRequired();
            builder.Property(v => v.ColaboradorId)
                   .HasColumnName("Colaborador_id")
                   .IsRequired();
            builder.Property(v => v.ViajeId)
                   .HasColumnName("Viaje_id")
                   .IsRequired();

            builder.HasOne(v => v.Colaborador)
                   .WithMany(c => c.Valoracionesviajes)
                   .HasForeignKey(v => v.ColaboradorId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasOne(v => v.Viaje)
                   .WithMany(vj => vj.Valoracionesviajes)
                   .HasForeignKey(v => v.ViajeId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
