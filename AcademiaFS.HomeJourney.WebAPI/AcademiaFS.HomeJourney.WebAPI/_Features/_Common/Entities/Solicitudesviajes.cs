using System;
using System.Collections.Generic;

namespace AcademiaFS.HomeJourney.WebAPI._Features._Common.Entities;

public partial class Solicitudesviajes
{
    public int SolicitudviajeId { get; set; }

    public int ColaboradorId { get; set; }

    public DateOnly Fechasolicitud { get; set; }

    public int ViajeId { get; set; }

    public int EstadoId { get; set; }

    public string? Comentarios { get; set; }

    public bool Activo { get; set; }

    public int Usuariocrea { get; set; }

    public DateTime Fechacrea { get; set; }

    public int? Usuariomodifica { get; set; }

    public DateTime? Fechamodifica { get; set; }

    public virtual Colaboradorses Colaborador { get; set; } = null!;

    public virtual Estados Estado { get; set; } = null!;

    public virtual Usuarios UsuariocreaNavigation { get; set; } = null!;

    public virtual Usuarios? UsuariomodificaNavigation { get; set; }

    public virtual Viajes Viaje { get; set; } = null!;
}
