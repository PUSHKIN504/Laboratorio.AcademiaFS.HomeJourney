using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Maps.Generals
{
    public class PersonasMap : IEntityTypeConfiguration<Personas>
    {
        public void Configure(EntityTypeBuilder<Personas> builder)
        {
            builder.ToTable("Personas");
            builder.HasKey(p => p.PersonaId);
            builder.Property(p => p.PersonaId)
                   .HasColumnName("Persona_id")
                   .IsRequired();
            builder.Property(p => p.Nombre)
                   .HasColumnName("Nombre")
                   .HasMaxLength(100)
                   .IsRequired();
            builder.Property(p => p.Apelllido)
                   .HasColumnName("Apelllido")
                   .HasMaxLength(100)
                   .IsRequired();
            builder.Property(p => p.Sexo)
                   .HasColumnName("Sexo")
                   .HasMaxLength(1)
                   .IsRequired();
            builder.Property(p => p.Email)
                   .HasColumnName("Email")
                   .HasMaxLength(100)
                   .IsRequired();
            builder.Property(p => p.Documentonacionalidentificacion)
                   .HasColumnName("Documentonacionalidentificacion")
                   .HasMaxLength(15)
                   .IsRequired();
            builder.Property(p => p.Activo)
                   .HasColumnName("Activo")
                   .IsRequired();
            builder.Property(p => p.EstadocivilId)
                   .HasColumnName("Estadocivil_id")
                   .IsRequired(false);
            builder.Property(p => p.CiudadId)
                   .HasColumnName("Ciudad_id")
                   .IsRequired();
            builder.Property(p => p.Usuariocrea)
                   .HasColumnName("Usuariocrea")
                   .IsRequired();
            builder.Property(p => p.Fechacrea)
                   .HasColumnName("Fechacrea")
                   .HasColumnType("datetime")
                   .IsRequired();
            builder.Property(p => p.Usuariomodifica)
                   .HasColumnName("Usuariomodifica")
                   .IsRequired(false);
            builder.Property(p => p.Fechamodifica)
                   .HasColumnName("Fechamodifica")
                   .HasColumnType("datetime")
                   .IsRequired(false);

            builder.HasOne(p => p.Ciudad)
                   .WithMany(c => c.Personas)
                   .HasForeignKey(p => p.CiudadId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasOne(p => p.Estadocivil)
                   .WithMany(ec => ec.Personas)
                   .HasForeignKey(p => p.EstadocivilId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relación uno a uno con Transportistas (si aplica)
            //builder.HasOne(p => p.Transportista)
            //       .WithOne(t => t.Persona)
            //       .HasForeignKey<Transportistas>(t => t.PersonaId)
            //       .HasPrincipalKey<Personas>(p => p.PersonaId)
            //       .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
