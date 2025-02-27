namespace AcademiaFS.HomeJourney.WebAPI._Features.Generals.Dto
{

    public class CiudadesDto
    {
        public int CiudadId { get; set; }
        public string Nombre { get; set; } = null!;
        public int DepartamentoId { get; set; }
        public bool Activo { get; set; }
    }
}
