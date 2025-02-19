using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Maps.Generals
{
    public class DepartamentosMap : IEntityTypeConfiguration<Departamentos>
    {
        public void Configure(EntityTypeBuilder<Departamentos> builder)
        {
            builder.ToTable("Departamentos");
            builder.HasKey(d => d.DepartamentoId);
            builder.Property(d => d.DepartamentoId)
                   .HasColumnName("Departamento_id")
                   .IsRequired();
            builder.Property(d => d.Nombre)
                   .HasColumnName("Nombre")
                   .HasMaxLength(75)
                   .IsRequired();
            builder.Property(d => d.Activo)
                   .HasColumnName("Activo")
                   .IsRequired();
            builder.Property(d => d.PaisId)
                   .HasColumnName("Pais_Id")
                   .IsRequired(false);

            builder.HasOne(d => d.Pais)
                   .WithMany(p => p.Departamentos)
                   .HasForeignKey(d => d.PaisId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
