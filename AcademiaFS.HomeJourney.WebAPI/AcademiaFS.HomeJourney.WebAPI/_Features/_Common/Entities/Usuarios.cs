using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI._Features._Common.Entities;

public partial class Usuarios
{
    public int UsuarioId { get; set; }

    public string Username { get; set; } = null!;

    public byte[] Passwordhash { get; set; } = null!;

    public int ColaboradorId { get; set; }

    public bool Esadmin { get; set; }

    public bool Activo { get; set; }

    public virtual Colaboradorses Colaborador { get; set; } = null!;

    public virtual ICollection<Colaboradorses> ColaboradoreUsuariocreaNavigations { get; set; } = new List<Colaboradorses>();

    public virtual ICollection<Colaboradorses> ColaboradoreUsuariomodificaNavigations { get; set; } = new List<Colaboradorses>();

    public virtual ICollection<Colaboradoressucursales> ColaboradoressucursaleUsuariocreaNavigations { get; set; } = new List<Colaboradoressucursales>();

    public virtual ICollection<Colaboradoressucursales> ColaboradoressucursaleUsuariomodificaNavigations { get; set; } = new List<Colaboradoressucursales>();

    public virtual ICollection<Pantalla> PantallaUsuariocreaNavigations { get; set; } = new List<Pantalla>();

    public virtual ICollection<Pantalla> PantallaUsuariomodificaNavigations { get; set; } = new List<Pantalla>();

    public virtual ICollection<Personas> PersonaUsuariocreaNavigations { get; set; } = new List<Personas>();

    public virtual ICollection<Personas> PersonaUsuariomodificaNavigations { get; set; } = new List<Personas>();

    public virtual ICollection<Roles> RoleUsuariocreaNavigations { get; set; } = new List<Roles>();

    public virtual ICollection<Roles> RoleUsuariomodificaNavigations { get; set; } = new List<Roles>();

    public virtual ICollection<Serviciostransportes> ServiciostransporteUsuariocreaNavigations { get; set; } = new List<Serviciostransportes>();

    public virtual ICollection<Serviciostransportes> ServiciostransporteUsuariomodificaNavigations { get; set; } = new List<Serviciostransportes>();

    public virtual ICollection<Solicitudesviajes> SolicitudesviajeUsuariocreaNavigations { get; set; } = new List<Solicitudesviajes>();

    public virtual ICollection<Solicitudesviajes> SolicitudesviajeUsuariomodificaNavigations { get; set; } = new List<Solicitudesviajes>();

    public virtual ICollection<Sucursales> SucursaleUsuariocreaNavigations { get; set; } = new List<Sucursales>();

    public virtual ICollection<Sucursales> SucursaleUsuariomodificaNavigations { get; set; } = new List<Sucursales>();

    public virtual ICollection<Transportistas> TransportistaUsuariocreaNavigations { get; set; } = new List<Transportistas>();

    public virtual ICollection<Transportistas> TransportistaUsuariomodificaNavigations { get; set; } = new List<Transportistas>();

    public virtual ICollection<Viajes> ViajeUsuariocreaNavigations { get; set; } = new List<Viajes>();

    public virtual ICollection<Viajes> ViajeUsuariomodificaNavigations { get; set; } = new List<Viajes>();

    public virtual ICollection<Viajesdetalles> ViajesdetalleUsuariocreaNavigations { get; set; } = new List<Viajesdetalles>();

    public virtual ICollection<Viajesdetalles> ViajesdetalleUsuariomodificaNavigations { get; set; } = new List<Viajesdetalles>();
}
