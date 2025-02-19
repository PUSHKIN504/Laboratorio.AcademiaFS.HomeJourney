namespace AcademiaFS.HomeJourney.WebAPI._Features.Auth.Dto
{
    public class UsuarioDto
    {
        public int UsuarioId { get; set; }
        public string Username { get; set; } = null!;
        public int ColaboradorId { get; set; }
        public bool Esadmin { get; set; }
        public bool Activo { get; set; }
    }
}
