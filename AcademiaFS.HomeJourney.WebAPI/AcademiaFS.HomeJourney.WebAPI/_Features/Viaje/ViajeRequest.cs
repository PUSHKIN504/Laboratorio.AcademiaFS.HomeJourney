using AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;

namespace AcademiaFS.HomeJourney.WebAPI._Features.Viaje
{
    public class ViajeRequest
    {
        public class CreateViajesRequest
        {
            public ViajesCreateClusteredDto viajeclusteredDto { get; set; }
            public List<List<ViajesdetallesCreateClusteredDto>> empleadosclusteredDto { get; set; }
        }
    }
}
