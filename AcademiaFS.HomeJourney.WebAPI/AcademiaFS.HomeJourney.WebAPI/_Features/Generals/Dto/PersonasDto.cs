namespace AcademiaFS.HomeJourney.WebAPI._Features.Generals.Dto
{
    public class PersonaDto
    {
        public int PersonaId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apelllido { get; set; } = null!;
        public string Sexo { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Documentonacionalidentificacion { get; set; } = null!;
        public bool Activo { get; set; }
        public int? EstadocivilId { get; set; }
        public int CiudadId { get; set; }
        public int Usuariocrea { get; set; }
        public DateTime Fechacrea { get; set; }
        public int? Usuariomodifica { get; set; }
        public DateTime? Fechamodifica { get; set; }
    }
}
