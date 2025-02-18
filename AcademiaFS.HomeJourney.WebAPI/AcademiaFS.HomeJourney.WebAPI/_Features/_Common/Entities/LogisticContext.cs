using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFS.HomeJourney.WebAPI._Features._Common.Entities;

public partial class LogisticContext : DbContext
{
    public LogisticContext()
    {
    }

    public LogisticContext(DbContextOptions<LogisticContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<Ciudade> Ciudades { get; set; }

    public virtual DbSet<Colaboradore> Colaboradores { get; set; }

    public virtual DbSet<Colaboradoressucursale> Colaboradoressucursales { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Estadoscivile> Estadosciviles { get; set; }

    public virtual DbSet<Moneda> Monedas { get; set; }

    public virtual DbSet<Paise> Paises { get; set; }

    public virtual DbSet<Pantalla> Pantallas { get; set; }

    public virtual DbSet<Pantallasrole> Pantallasroles { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Serviciostransporte> Serviciostransportes { get; set; }

    public virtual DbSet<Solicitudesviaje> Solicitudesviajes { get; set; }

    public virtual DbSet<Sucursale> Sucursales { get; set; }

    public virtual DbSet<Transportista> Transportistas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Valoracionesviaje> Valoracionesviajes { get; set; }

    public virtual DbSet<Viaje> Viajes { get; set; }

    public virtual DbSet<Viajesdetalle> Viajesdetalles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=192.168.1.33\\academiagfs,49194;Database=HomeJourney;User Id=AcademiaDEV;Password=Academia.1;Encrypt=True;TrustServerCertificate=True;Connect Timeout=120;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_100_CI_AS");

        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.CargoId).HasName("PK_Cargos_Cargo_id");

            entity.Property(e => e.CargoId).HasColumnName("Cargo_id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Ciudade>(entity =>
        {
            entity.HasKey(e => e.CiudadId).HasName("PK_Ciudades_Ciudad_id");

            entity.Property(e => e.CiudadId).HasColumnName("Ciudad_id");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.DepartamentoId).HasColumnName("Departamento_id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(75)
                .IsUnicode(false);

            entity.HasOne(d => d.Departamento).WithMany(p => p.Ciudades)
                .HasForeignKey(d => d.DepartamentoId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Colaboradore>(entity =>
        {
            entity.HasKey(e => e.ColaboradorId).HasName("PK_Colaboradores_Colaborador_id");

            entity.HasIndex(e => e.PersonaId, "UQ_Colaboradores_Persona_id").IsUnique();

            entity.Property(e => e.ColaboradorId).HasColumnName("Colaborador_id");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.CargoId).HasColumnName("Cargo_id");
            entity.Property(e => e.Direccion)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Fechacrea).HasColumnType("datetime");
            entity.Property(e => e.Fechamodifica).HasColumnType("datetime");
            entity.Property(e => e.Latitud).HasColumnType("decimal(9, 8)");
            entity.Property(e => e.Longitud).HasColumnType("decimal(9, 8)");
            entity.Property(e => e.PersonaId).HasColumnName("Persona_id");
            entity.Property(e => e.RolId).HasColumnName("Rol_id");

            entity.HasOne(d => d.Cargo).WithMany(p => p.Colaboradores)
                .HasForeignKey(d => d.CargoId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Persona).WithOne(p => p.Colaboradore)
                .HasForeignKey<Colaboradore>(d => d.PersonaId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Rol).WithMany(p => p.Colaboradores)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.UsuariocreaNavigation).WithMany(p => p.ColaboradoreUsuariocreaNavigations)
                .HasForeignKey(d => d.Usuariocrea)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.UsuariomodificaNavigation).WithMany(p => p.ColaboradoreUsuariomodificaNavigations).HasForeignKey(d => d.Usuariomodifica);
        });

        modelBuilder.Entity<Colaboradoressucursale>(entity =>
        {
            entity.HasKey(e => e.ColaboradorsucursalId).HasName("PK_Colaboradoressucursales_Colaboradorsucursal_id");

            entity.HasIndex(e => new { e.ColaboradorId, e.SucursalId }, "UQ_ColaboradorSucursal").IsUnique();

            entity.Property(e => e.ColaboradorsucursalId).HasColumnName("Colaboradorsucursal_id");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.ColaboradorId).HasColumnName("Colaborador_id");
            entity.Property(e => e.Distanciakilometro).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Fechacrea).HasColumnType("datetime");
            entity.Property(e => e.Fechamodifica).HasColumnType("datetime");
            entity.Property(e => e.SucursalId).HasColumnName("Sucursal_id");

            entity.HasOne(d => d.Colaborador).WithMany(p => p.Colaboradoressucursales)
                .HasForeignKey(d => d.ColaboradorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Colaboradores");

            entity.HasOne(d => d.Sucursal).WithMany(p => p.Colaboradoressucursales)
                .HasForeignKey(d => d.SucursalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sucursales");

            entity.HasOne(d => d.UsuariocreaNavigation).WithMany(p => p.ColaboradoressucursaleUsuariocreaNavigations)
                .HasForeignKey(d => d.Usuariocrea)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.UsuariomodificaNavigation).WithMany(p => p.ColaboradoressucursaleUsuariomodificaNavigations).HasForeignKey(d => d.Usuariomodifica);
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.DepartamentoId).HasName("PK_Departamentos_Departamento_id");

            entity.Property(e => e.DepartamentoId).HasColumnName("Departamento_id");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Nombre)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.PaisId).HasColumnName("Pais_Id");

            entity.HasOne(d => d.Pais).WithMany(p => p.Departamentos)
                .HasForeignKey(d => d.PaisId)
                .HasConstraintName("FK_Departamento_Paises_Pais_Id");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.EstadoId).HasName("PK_Estados_Estado_id");

            entity.Property(e => e.EstadoId).HasColumnName("Estado_id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Estadoscivile>(entity =>
        {
            entity.HasKey(e => e.EstadocivilId).HasName("PK_Estadosciviles_Estadocivil_id");

            entity.Property(e => e.EstadocivilId).HasColumnName("Estadocivil_id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Moneda>(entity =>
        {
            entity.HasKey(e => e.MonedaId).HasName("PK_Monedas_Moneda_Id");

            entity.Property(e => e.MonedaId).HasColumnName("Moneda_Id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Simbolo).HasMaxLength(1);
            entity.Property(e => e.ValorLempiras).HasColumnType("smallmoney");
        });

        modelBuilder.Entity<Paise>(entity =>
        {
            entity.HasKey(e => e.PaisId).HasName("PK_Paises_Pais_id");

            entity.Property(e => e.PaisId).HasColumnName("Pais_id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Pantalla>(entity =>
        {
            entity.HasKey(e => e.PantallaId).HasName("PK_Pantallas_Pantalla_id");

            entity.Property(e => e.PantallaId).HasColumnName("Pantalla_id");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Fechacrea).HasColumnType("datetime");
            entity.Property(e => e.Fechamodifica).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.UsuariocreaNavigation).WithMany(p => p.PantallaUsuariocreaNavigations)
                .HasForeignKey(d => d.Usuariocrea)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.UsuariomodificaNavigation).WithMany(p => p.PantallaUsuariomodificaNavigations).HasForeignKey(d => d.Usuariomodifica);
        });

        modelBuilder.Entity<Pantallasrole>(entity =>
        {
            entity.HasKey(e => e.PantallarolId).HasName("PK_Pantallasroles_Pantallarol_id");

            entity.Property(e => e.PantallarolId).HasColumnName("Pantallarol_id");
            entity.Property(e => e.Fechacrea).HasColumnType("datetime");
            entity.Property(e => e.PantallaId).HasColumnName("Pantalla_id");
            entity.Property(e => e.RolId).HasColumnName("Rol_id");

            entity.HasOne(d => d.Pantalla).WithMany(p => p.Pantallasroles)
                .HasForeignKey(d => d.PantallaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pantallas_Roles_Pantallas_Pantalla_id");

            entity.HasOne(d => d.Rol).WithMany(p => p.Pantallasroles)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pantallas_Roles_Roles_Rol_id");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.PersonaId).HasName("PK_Personas_Persona_id");

            entity.HasIndex(e => e.Email, "UQ__Personas__A9D10534B64E690E").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Personas__A9D10534EF0F0958").IsUnique();

            entity.Property(e => e.PersonaId).HasColumnName("Persona_id");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Apelllido)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CiudadId).HasColumnName("Ciudad_id");
            entity.Property(e => e.Documentonacionalidentificacion)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EstadocivilId).HasColumnName("Estadocivil_id");
            entity.Property(e => e.Fechacrea).HasColumnType("datetime");
            entity.Property(e => e.Fechamodifica).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Sexo)
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.HasOne(d => d.Ciudad).WithMany(p => p.Personas)
                .HasForeignKey(d => d.CiudadId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Estadocivil).WithMany(p => p.Personas).HasForeignKey(d => d.EstadocivilId);

            entity.HasOne(d => d.UsuariocreaNavigation).WithMany(p => p.PersonaUsuariocreaNavigations)
                .HasForeignKey(d => d.Usuariocrea)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.UsuariomodificaNavigation).WithMany(p => p.PersonaUsuariomodificaNavigations).HasForeignKey(d => d.Usuariomodifica);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK_Roles_Rol_id");

            entity.Property(e => e.RolId).HasColumnName("Rol_id");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Fechacrea).HasColumnType("datetime");
            entity.Property(e => e.Fechamodifica).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.UsuariocreaNavigation).WithMany(p => p.RoleUsuariocreaNavigations)
                .HasForeignKey(d => d.Usuariocrea)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.UsuariomodificaNavigation).WithMany(p => p.RoleUsuariomodificaNavigations).HasForeignKey(d => d.Usuariomodifica);
        });

        modelBuilder.Entity<Serviciostransporte>(entity =>
        {
            entity.HasKey(e => e.ServiciotransporteId).HasName("PK_Serviciostrasnporte_Serviciotransporte_id");

            entity.ToTable("Serviciostransporte");

            entity.HasIndex(e => e.Email, "UQ__Servicio__A9D105340503BCB2").IsUnique();

            entity.Property(e => e.ServiciotransporteId).HasColumnName("Serviciotransporte_id");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Fechacrea).HasColumnType("datetime");
            entity.Property(e => e.Fechamodifica).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.UsuariocreaNavigation).WithMany(p => p.ServiciostransporteUsuariocreaNavigations)
                .HasForeignKey(d => d.Usuariocrea)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.UsuariomodificaNavigation).WithMany(p => p.ServiciostransporteUsuariomodificaNavigations).HasForeignKey(d => d.Usuariomodifica);
        });

        modelBuilder.Entity<Solicitudesviaje>(entity =>
        {
            entity.HasKey(e => e.SolicitudviajeId).HasName("PK_Solicitudesviajes_Solicitudviaje_id");

            entity.Property(e => e.SolicitudviajeId).HasColumnName("Solicitudviaje_id");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.ColaboradorId).HasColumnName("Colaborador_id");
            entity.Property(e => e.Comentarios)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.EstadoId).HasColumnName("Estado_id");
            entity.Property(e => e.Fechacrea).HasColumnType("datetime");
            entity.Property(e => e.Fechamodifica).HasColumnType("datetime");
            entity.Property(e => e.ViajeId).HasColumnName("Viaje_id");

            entity.HasOne(d => d.Colaborador).WithMany(p => p.Solicitudesviajes)
                .HasForeignKey(d => d.ColaboradorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SolicitudesViaje_Colaboradores_Colaborador_id");

            entity.HasOne(d => d.Estado).WithMany(p => p.Solicitudesviajes)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SolicitudesViaje_EstadosSolicitud_Estadosolicitud_id");

            entity.HasOne(d => d.UsuariocreaNavigation).WithMany(p => p.SolicitudesviajeUsuariocreaNavigations)
                .HasForeignKey(d => d.Usuariocrea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SolicitudesViaje_Usuarios_Usuariocrea_Usuariocrea");

            entity.HasOne(d => d.UsuariomodificaNavigation).WithMany(p => p.SolicitudesviajeUsuariomodificaNavigations)
                .HasForeignKey(d => d.Usuariomodifica)
                .HasConstraintName("FK_SolicitudesViaje_Usuarios_Usuariomodifica_Usuariomodifica");

            entity.HasOne(d => d.Viaje).WithMany(p => p.Solicitudesviajes)
                .HasForeignKey(d => d.ViajeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SolicitudesViaje_Viajes_Viaje_id");
        });

        modelBuilder.Entity<Sucursale>(entity =>
        {
            entity.HasKey(e => e.SucursalId).HasName("PK_Sucursales_Sucursal_id");

            entity.Property(e => e.SucursalId).HasColumnName("Sucursal_id");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Fechacrea).HasColumnType("datetime");
            entity.Property(e => e.Fechamodifica).HasColumnType("datetime");
            entity.Property(e => e.Latitud).HasColumnType("decimal(9, 8)");
            entity.Property(e => e.Longitud).HasColumnType("decimal(9, 8)");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.UsuariocreaNavigation).WithMany(p => p.SucursaleUsuariocreaNavigations)
                .HasForeignKey(d => d.Usuariocrea)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.UsuariomodificaNavigation).WithMany(p => p.SucursaleUsuariomodificaNavigations).HasForeignKey(d => d.Usuariomodifica);
        });

        modelBuilder.Entity<Transportista>(entity =>
        {
            entity.HasKey(e => e.TransportistaId).HasName("PK_Transportista_Transportista_id");

            entity.HasIndex(e => e.PersonaId, "UQ_Transportistas_Persona_id").IsUnique();

            entity.Property(e => e.TransportistaId).HasColumnName("Transportista_id");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Fechacrea).HasColumnType("datetime");
            entity.Property(e => e.Fechamodifica).HasColumnType("datetime");
            entity.Property(e => e.MonedaId).HasColumnName("Moneda_Id");
            entity.Property(e => e.PersonaId).HasColumnName("Persona_id");
            entity.Property(e => e.ServiciotransporteId).HasColumnName("Serviciotransporte_id");
            entity.Property(e => e.Tarifaporkilometro).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Persona).WithOne(p => p.Transportista)
                .HasForeignKey<Transportista>(d => d.PersonaId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Serviciotransporte).WithMany(p => p.Transportista)
                .HasForeignKey(d => d.ServiciotransporteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transportistas_Serviciostransporte");

            entity.HasOne(d => d.UsuariocreaNavigation).WithMany(p => p.TransportistaUsuariocreaNavigations)
                .HasForeignKey(d => d.Usuariocrea)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.UsuariomodificaNavigation).WithMany(p => p.TransportistaUsuariomodificaNavigations).HasForeignKey(d => d.Usuariomodifica);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK_Usuarios_Usuario_id");

            entity.HasIndex(e => e.ColaboradorId, "UQ__Usuarios__46FD4C9B3B5A1C28").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__Usuarios__536C85E429221941").IsUnique();

            entity.Property(e => e.UsuarioId).HasColumnName("Usuario_id");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.ColaboradorId).HasColumnName("Colaborador_id");
            entity.Property(e => e.Passwordhash).HasMaxLength(32);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Colaborador).WithOne(p => p.Usuario)
                .HasForeignKey<Usuario>(d => d.ColaboradorId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Valoracionesviaje>(entity =>
        {
            entity.HasKey(e => e.ValoracionviajeId).HasName("PK_Valoracionesviajes_Valoracionviaje_id");

            entity.Property(e => e.ValoracionviajeId).HasColumnName("Valoracionviaje_id");
            entity.Property(e => e.ColaboradorId).HasColumnName("Colaborador_id");
            entity.Property(e => e.ViajeId).HasColumnName("Viaje_id");

            entity.HasOne(d => d.Colaborador).WithMany(p => p.Valoracionesviajes)
                .HasForeignKey(d => d.ColaboradorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Valoracionviaje_Colaboradores_Colaborador_id");

            entity.HasOne(d => d.Viaje).WithMany(p => p.Valoracionesviajes)
                .HasForeignKey(d => d.ViajeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Valoracionviaje_Viajes_Viaje_id");
        });

        modelBuilder.Entity<Viaje>(entity =>
        {
            entity.HasKey(e => e.ViajeId).HasName("PK_Viajes_Viajes_id");

            entity.Property(e => e.ViajeId).HasColumnName("Viaje_id");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.EstadoId).HasColumnName("Estado_id");
            entity.Property(e => e.Fechacrea).HasColumnType("datetime");
            entity.Property(e => e.Fechamodifica).HasColumnType("datetime");
            entity.Property(e => e.MonedaId).HasColumnName("Moneda_Id");
            entity.Property(e => e.SucursalId).HasColumnName("Sucursal_id");
            entity.Property(e => e.Totalkilometros).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Totalpagar).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TransportistaId).HasColumnName("Transportista_id");

            entity.HasOne(d => d.Estado).WithMany(p => p.Viajes)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Moneda).WithMany(p => p.Viajes)
                .HasForeignKey(d => d.MonedaId)
                .HasConstraintName("FK_Transportista_Monedas_Moneda_Id");

            entity.HasOne(d => d.Sucursal).WithMany(p => p.Viajes)
                .HasForeignKey(d => d.SucursalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Viajes__Sucursal__078C1F06");

            entity.HasOne(d => d.Transportista).WithMany(p => p.Viajes)
                .HasForeignKey(d => d.TransportistaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Viajes__Transpor__0880433F");

            entity.HasOne(d => d.UsuariocreaNavigation).WithMany(p => p.ViajeUsuariocreaNavigations)
                .HasForeignKey(d => d.Usuariocrea)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.UsuariomodificaNavigation).WithMany(p => p.ViajeUsuariomodificaNavigations).HasForeignKey(d => d.Usuariomodifica);
        });

        modelBuilder.Entity<Viajesdetalle>(entity =>
        {
            entity.HasKey(e => e.ViajedetalleId).HasName("PK_Viajesdetalles_Viajedetalle_id");

            entity.Property(e => e.ViajedetalleId).HasColumnName("Viajedetalle_id");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.ColaboradorId).HasColumnName("Colaborador_id");
            entity.Property(e => e.ColaboradorsucursalId).HasColumnName("Colaboradorsucursal_id");
            entity.Property(e => e.Distanciakilometros).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Fechacrea).HasColumnType("datetime");
            entity.Property(e => e.Fechamodifica).HasColumnType("datetime");
            entity.Property(e => e.MonedaId).HasColumnName("Moneda_Id");
            entity.Property(e => e.Totalpagar).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ViajeId).HasColumnName("Viaje_id");

            entity.HasOne(d => d.Colaboradorsucursal).WithMany(p => p.Viajesdetalles)
                .HasForeignKey(d => d.ColaboradorsucursalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Viajesdetalles_Colaboradores_por_sucursales_Colaboradorsucursal_id");

            entity.HasOne(d => d.Moneda).WithMany(p => p.Viajesdetalles).HasForeignKey(d => d.MonedaId);

            entity.HasOne(d => d.UsuariocreaNavigation).WithMany(p => p.ViajesdetalleUsuariocreaNavigations)
                .HasForeignKey(d => d.Usuariocrea)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.UsuariomodificaNavigation).WithMany(p => p.ViajesdetalleUsuariomodificaNavigations).HasForeignKey(d => d.Usuariomodifica);

            entity.HasOne(d => d.Viaje).WithMany(p => p.Viajesdetalles)
                .HasForeignKey(d => d.ViajeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Viajesdet__Viaje__09746778");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
