using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Laboratorio.Academina.JasonVillanueva.WebAPI.Infrastructure.BDName.Maps._Common
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario> 
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Agentes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Agente_ID").IsRequired();
            builder.Property(x => x.Nombre).HasColumnName("Agente_Nombre").IsRequired();
        }
    }
}
