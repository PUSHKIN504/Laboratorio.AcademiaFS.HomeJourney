using System.Text.Json.Serialization;

namespace AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto
{
    public class Duration
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("value")]
        public int Value { get; set; }
    }
}
