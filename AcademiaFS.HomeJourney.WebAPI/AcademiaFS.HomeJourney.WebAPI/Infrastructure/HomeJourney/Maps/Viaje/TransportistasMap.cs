using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Maps.Viaje
{
    public class TransportistasMap : IEntityTypeConfiguration<Transportistas>
    {
        public void Configure(EntityTypeBuilder<Transportistas> builder)
        {
            builder.ToTable("Transportistas");
            builder.HasKey(t => t.TransportistaId);
            builder.Property(t => t.TransportistaId)
                   .HasColumnName("Transportista_id")
                   .IsRequired();
            builder.Property(t => t.ServiciotransporteId)
                   .HasColumnName("Serviciotransporte_id")
                   .IsRequired();
            builder.Property(t => t.Tarifaporkilometro)
                   .HasColumnName("Tarifaporkilometro")
                   .HasColumnType("decimal(10,2)")
                   .IsRequired();
            builder.Property(t => t.Activo)
                   .HasColumnName("Activo")
                   .IsRequired();
            builder.Property(t => t.Usuariocrea)
                   .HasColumnName("Usuariocrea")
                   .IsRequired();
            builder.Property(t => t.Fechacrea)
                   .HasColumnName("Fechacrea")
                   .HasColumnType("datetime")
                   .IsRequired();
            builder.Property(t => t.PersonaId)
                   .HasColumnName("Persona_id")
                   .IsRequired();
            builder.Property(t => t.Usuariomodifica)
                   .HasColumnName("Usuariomodifica")
                   .IsRequired(false);
            builder.Property(t => t.Fechamodifica)
                   .HasColumnName("Fechamodifica")
                   .HasColumnType("datetime")
                   .IsRequired(false);
            builder.Property(t => t.MonedaId)
                   .HasColumnName("Moneda_Id")
                   .IsRequired(false);

            builder.HasOne(t => t.Serviciotransporte)
                   .WithMany()
                   .HasForeignKey(t => t.ServiciotransporteId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Configurar la relación uno a uno entre Transportistas y Personas.
            builder.HasOne(t => t.Persona)
                   .WithMany(p => p.Transportista)
                   .HasForeignKey(t => t.PersonaId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
