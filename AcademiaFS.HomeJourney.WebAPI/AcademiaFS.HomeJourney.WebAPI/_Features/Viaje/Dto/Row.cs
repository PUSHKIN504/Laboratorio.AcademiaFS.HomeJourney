using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto
{
    [ExcludeFromCodeCoverage]
    public class Row
    {
        [JsonPropertyName("elements")]
        public Element[] Elements { get; set; }
    }
}
