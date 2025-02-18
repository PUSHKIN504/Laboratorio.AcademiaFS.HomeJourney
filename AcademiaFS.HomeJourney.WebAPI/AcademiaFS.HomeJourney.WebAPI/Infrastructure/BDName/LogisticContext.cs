using Laboratorio.Academina.JasonVillanueva.WebAPI._Features._Common.Entities;
using Laboratorio.Academina.JasonVillanueva.WebAPI.Infrastructure.BDName.Maps._Common;
using Microsoft.EntityFrameworkCore;

namespace Laboratorio.Academina.JasonVillanueva.WebAPI.Infrastructure.BDName
{
    public class LogisticContext : DbContext
    {
        public LogisticContext(DbContextOptions<LogisticContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios => Set<Usuario>();

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new UsuarioMap());

        }
    }
}
