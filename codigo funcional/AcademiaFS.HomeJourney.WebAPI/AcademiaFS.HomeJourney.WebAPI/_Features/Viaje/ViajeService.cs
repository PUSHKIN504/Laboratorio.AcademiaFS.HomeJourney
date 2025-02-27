using AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFS.HomeJourney.WebAPI._Features.Viaje
{
    public class ViajesService
    {
        private readonly HomeJourneyContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DomainServiceClustering _clusteringService;

        public ViajesService(HomeJourneyContext context,
                             IUnitOfWork unitOfWork,
                             DomainServiceClustering clusteringService)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _clusteringService = clusteringService;
        }

        /// <summary>
        /// Ejemplo de validación: solo cargo = 3 (Gerente de tienda) puede agrupar colaboradores.
        /// </summary>
        public async Task<List<List<ViajesdetallesCreateClusteredDto>>> ClusterEmployeesAsync(
            List<ViajesdetallesCreateClusteredDto> employees,
            decimal distanceThreshold,
            int usuarioCrea) // ID del usuario que hace la petición
        {
            // Verificamos que el usuario tenga cargo = 3
            var colaborador = await (from col in _context.Colaboradores
                                     join usr in _context.Usuarios on col.ColaboradorId equals usr.ColaboradorId
                                     where usr.UsuarioId == usuarioCrea
                                     select col).FirstOrDefaultAsync();

            if (colaborador == null)
                throw new Exception("No se encontró un colaborador asociado a este usuario.");

            //if (colaborador.CargoId != 3)
            //    throw new Exception("Solo un usuario con cargo 'Gerente de tienda' puede agrupar colaboradores.");

            // Llamamos al servicio de clustering
            return await _clusteringService.ClusterEmployeesAsync(employees, distanceThreshold);
        }

        //public async Task<List<Viajes>> CreateTripsFromClustersAsync(
        //    ViajesCreateClusteredDto tripDto,
        //    List<List<ViajesdetallesCreateClusteredDto>> clusteredEmployees)
        //{
        //    // 1. Validamos cargo = 3 para el usuario que crea
        //    var colaborador = await (from col in _context.Colaboradores
        //                             join usr in _context.Usuarios on col.ColaboradorId equals usr.ColaboradorId
        //                             where usr.UsuarioId == tripDto.Usuariocrea
        //                             select col).FirstOrDefaultAsync();

        //    if (colaborador == null)
        //        throw new Exception("No se encontró un colaborador asociado a este usuario.");

        //    //if (colaborador.CargoId != 3)
        //    //    throw new Exception("Solo un usuario con cargo 'Gerente de tienda' puede crear viajes.");

        //    // 2. Obtenemos viajes abiertos en la sucursal de la fecha dada
        //    var viajesAbiertos = await _context.Viajes
        //        .Include(v => v.Viajesdetalles)
        //        .Where(v =>
        //            v.SucursalId == tripDto.SucursalId &&
        //            v.EstadoId == 1004 &&
        //            v.Activo &&
        //            EF.Functions.DateDiffDay(v.Viajefecha, tripDto.Viajefecha) == 0
        //        )
        //        .ToListAsync();

        //    // 3. Función local para saber si un colaborador viaja en esta fecha
        //    bool ColaboradorYaViajaHoy(int colaboradorId, DateTime fecha)
        //    {
        //        return false;
        //            //_context.Viajesdetalles
        //            //.Any(vd => vd.ColaboradorId == colaboradorId
        //            //           && vd.Activo
        //            //           && vd.Viaje.Viajefecha == fecha);
        //    }

        //    // 4. Creamos viajes (o asignamos a viajes abiertos) a partir de los clusters
        //    var tripsToAdd = _clusteringService.CreateTripsFromClusters(
        //        tripDto,
        //        clusteredEmployees,
        //        viajesAbiertos,
        //        ColaboradorYaViajaHoy
        //    );

        //    // 5. Guardamos en una transacción
        //    await _unitOfWork.BeginTransactionAsync();

        //    try
        //    {
        //        // Primero guardamos los viajes abiertos que hayan sido modificados
        //        // (si agregamos detalles nuevos).
        //        // EFCore los rastrea, de modo que se detectarán cambios en sus Viajesdetalles.
        //        // Nada especial que hacer si EFTracking ya está habilitado, 
        //        // pero si no, habría que Attach manualmente.

        //        // Agregamos los nuevos viajes
        //        _unitOfWork.Context.Viajes.AddRange(tripsToAdd);

        //        var affectedRows = await _unitOfWork.SaveAsync();
        //        if (affectedRows == 0)
        //            throw new Exception("No se guardaron cambios en la base de datos.");

        //        await _unitOfWork.CommitTransactionAsync();

        //        // Como algunos colaboradores pudieron haberse agregado a viajes abiertos,
        //        // la lista final de viajes creados o modificados podría ser la combinación:
        //        //   * Los viajes abiertos (si agregamos gente).
        //        //   * Los nuevos viajes (tripsToAdd).
        //        // Aquí devolvemos ambos, o solo los nuevos, según la necesidad:
        //        return viajesAbiertos.Concat(tripsToAdd).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        await _unitOfWork.RollbackTransactionAsync();
        //        throw new Exception($"Error al guardar los viajes en una transacción: {ex.Message}", ex);
        //    }
        //}
        public async Task<List<Viajes>> CreateTripsFromClustersAsync(
            ViajesCreateClusteredDto tripDto,
            List<List<ViajesdetallesCreateClusteredDto>> clusteredEmployees,
            decimal distanceThreshold)
        {
            // 1. Validar que el usuario sea Gerente, etc. (igual que antes).
            var colaborador = await (from col in _context.Colaboradores
                                     join usr in _context.Usuarios on col.ColaboradorId equals usr.ColaboradorId
                                     where usr.UsuarioId == tripDto.Usuariocrea
                                     select col)
                                     .FirstOrDefaultAsync();
            if (colaborador == null)
                throw new Exception("No se encontró un colaborador asociado a este usuario.");
            //if (colaborador.CargoId != 3)
            //    throw new Exception("Solo un usuario con cargo 'Gerente de tienda' puede crear viajes.");

            // 2. Obtener viajes abiertos en la misma sucursal y fecha
            var viajesAbiertos = await _context.Viajes
                .Include(v => v.Viajesdetalles)
                    .ThenInclude(d => d.Colaboradorsucursal)
                        .ThenInclude(cs => cs.Colaborador)
                .Where(v =>
                    v.SucursalId == tripDto.SucursalId &&
                    v.Viajefecha == tripDto.Viajefecha &&
                    v.EstadoId == 1004 && // Solo los que estén en estado "abierto"
                    v.Activo
                )
                .ToListAsync();

            // 3. Función local para verificar viajes repetidos
            bool ColaboradorYaViajaHoy(int colaboradorId, DateTime fecha)
            {
                return _context.Viajesdetalles
                    .Any(vd => vd.ColaboradorId == colaboradorId
                               && vd.Activo
                               && vd.Viaje.Viajefecha == fecha);
            }

            // 4. Llamamos al clustering final
            var newTrips = _clusteringService.CreateTripsFromClusters(
                tripDto,
                clusteredEmployees,
                viajesAbiertos,
                ColaboradorYaViajaHoy,
                distanceThreshold // <= Aquí pasamos el umbral
            );

            // 5. Guardamos en transacción
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                _unitOfWork.Context.Viajes.AddRange(newTrips);
                var affected = await _unitOfWork.SaveAsync();
                if (affected == 0)
                    throw new Exception("No se guardaron cambios.");

                await _unitOfWork.CommitTransactionAsync();

                // Combinamos en la salida los viajes abiertos que pudieron haberse modificado + los nuevos
                return viajesAbiertos.Concat(newTrips).ToList();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception($"Error al guardar: {ex.Message}", ex);
            }
        }
        // Ejemplo de método existente
        public async Task<Viajes> CreateViajeWithDetailsAsync(Viajes viaje, List<Viajesdetalles> detalles)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Viajes.Add(viaje);
                    await _unitOfWork.SaveAsync();

                    foreach (var detalle in detalles)
                    {
                        detalle.ViajeId = viaje.ViajeId;
                        _context.Viajesdetalles.Add(detalle);
                    }
                    await _unitOfWork.SaveAsync();

                    await transaction.CommitAsync();
                    return viaje;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
