
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto
{
    [ExcludeFromCodeCoverage]
    public class DistanceMatrixResponse
    {

        [JsonPropertyName("destination_addresses")]
        public string[] DestinationAddresses { get; set; }

        [JsonPropertyName("origin_addresses")]
        public string[] OriginAddresses { get; set; }

        [JsonPropertyName("rows")]
        public Row[] Rows { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

    }
}
