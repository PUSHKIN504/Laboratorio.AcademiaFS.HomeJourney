namespace Laboratorio.Academina.JasonVillanueva.WebAPI._Features._Common.Entities
{
    public class Usuario
    {
        public string Id { get; set; }
        public string Nombre { get; set; }

        public Usuario(string id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
    }
}
