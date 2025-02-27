namespace AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto
{
    public class TransportistaDto
    {
        public int TransportistaId { get; set; }
        public int ServiciotransporteId { get; set; }
        public decimal Tarifaporkilometro { get; set; }
        public bool Activo { get; set; }
        public int Usuariocrea { get; set; }
        public DateTime Fechacrea { get; set; }
        public int PersonaId { get; set; }
        public int? Usuariomodifica { get; set; }
        public DateTime? Fechamodifica { get; set; }
        public int? MonedaId { get; set; }
    }
    public class CreateTransportistaDto
    {
        public string Nombre { get; set; } = null!;
        public string Apelllido { get; set; } = null!;
        public string Sexo { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Documentonacionalidentificacion { get; set; } = null!;
        public int CiudadId { get; set; }
        public int Usuariocrea { get; set; }

        public decimal Tarifaporkilometro { get; set; }
        public int ServiciotransporteId { get; set; }
        public int? MonedaId { get; set; }
    }
}
