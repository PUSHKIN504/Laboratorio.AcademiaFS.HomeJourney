using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI._Features._Common.Entities;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Username { get; set; } = null!;

    public byte[] Passwordhash { get; set; } = null!;

    public int ColaboradorId { get; set; }

    public bool Esadmin { get; set; }

    public bool Activo { get; set; }

    public virtual Colaboradore Colaborador { get; set; } = null!;

    public virtual ICollection<Colaboradore> ColaboradoreUsuariocreaNavigations { get; set; } = new List<Colaboradore>();

    public virtual ICollection<Colaboradore> ColaboradoreUsuariomodificaNavigations { get; set; } = new List<Colaboradore>();

    public virtual ICollection<Colaboradoressucursale> ColaboradoressucursaleUsuariocreaNavigations { get; set; } = new List<Colaboradoressucursale>();

    public virtual ICollection<Colaboradoressucursale> ColaboradoressucursaleUsuariomodificaNavigations { get; set; } = new List<Colaboradoressucursale>();

    public virtual ICollection<Pantalla> PantallaUsuariocreaNavigations { get; set; } = new List<Pantalla>();

    public virtual ICollection<Pantalla> PantallaUsuariomodificaNavigations { get; set; } = new List<Pantalla>();

    public virtual ICollection<Persona> PersonaUsuariocreaNavigations { get; set; } = new List<Persona>();

    public virtual ICollection<Persona> PersonaUsuariomodificaNavigations { get; set; } = new List<Persona>();

    public virtual ICollection<Role> RoleUsuariocreaNavigations { get; set; } = new List<Role>();

    public virtual ICollection<Role> RoleUsuariomodificaNavigations { get; set; } = new List<Role>();

    public virtual ICollection<Serviciostransporte> ServiciostransporteUsuariocreaNavigations { get; set; } = new List<Serviciostransporte>();

    public virtual ICollection<Serviciostransporte> ServiciostransporteUsuariomodificaNavigations { get; set; } = new List<Serviciostransporte>();

    public virtual ICollection<Solicitudesviaje> SolicitudesviajeUsuariocreaNavigations { get; set; } = new List<Solicitudesviaje>();

    public virtual ICollection<Solicitudesviaje> SolicitudesviajeUsuariomodificaNavigations { get; set; } = new List<Solicitudesviaje>();

    public virtual ICollection<Sucursale> SucursaleUsuariocreaNavigations { get; set; } = new List<Sucursale>();

    public virtual ICollection<Sucursale> SucursaleUsuariomodificaNavigations { get; set; } = new List<Sucursale>();

    public virtual ICollection<Transportista> TransportistaUsuariocreaNavigations { get; set; } = new List<Transportista>();

    public virtual ICollection<Transportista> TransportistaUsuariomodificaNavigations { get; set; } = new List<Transportista>();

    public virtual ICollection<Viaje> ViajeUsuariocreaNavigations { get; set; } = new List<Viaje>();

    public virtual ICollection<Viaje> ViajeUsuariomodificaNavigations { get; set; } = new List<Viaje>();

    public virtual ICollection<Viajesdetalle> ViajesdetalleUsuariocreaNavigations { get; set; } = new List<Viajesdetalle>();

    public virtual ICollection<Viajesdetalle> ViajesdetalleUsuariomodificaNavigations { get; set; } = new List<Viajesdetalle>();
}
