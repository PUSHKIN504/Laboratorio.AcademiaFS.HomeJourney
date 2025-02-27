using AcademiaFS.HomeJourney.WebAPI._Common;
using AcademiaFS.HomeJourney.WebAPI._Features.Viaje.Dto;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;

namespace AcademiaFS.HomeJourney.WebAPI._Features.Viaje
{
    public class DomainServiceClustering
    {
        private readonly GoogleMapsService _googleMapsService;
        private readonly Random _random = new Random();

        public DomainServiceClustering(GoogleMapsService googleMapsService)
        {
            _googleMapsService = googleMapsService;
        }

        /// <summary>
        /// Agrupa (cluster) a los colaboradores según la matriz de distancias de Google.
        /// </summary>
        public async Task<List<List<ViajesdetallesCreateClusteredDto>>> ClusterEmployeesAsync(
            List<ViajesdetallesCreateClusteredDto> employees,
            decimal distanceThreshold)
        {
            if (employees == null || !employees.Any())
                throw new ArgumentException("La lista de colaboradores no puede ser nula o vacía.");
            if (distanceThreshold <= 0)
                throw new ArgumentException("El umbral de distancia debe ser mayor que 0.");

            // Preparamos las ubicaciones para pasarlas al servicio de Google
            var locations = employees
                .Select(e => $"{e.Latitud},{e.Longitud}")
                .ToList();

            // Obtenemos la matriz de distancias
            var distanceMatrix = await _googleMapsService.GetDistanceMatrixAsync(locations);

            // Se procede al clustering jerárquico con base en la matriz
            var clusters = HierarchicalClustering(distanceMatrix, (double)distanceThreshold);

            // Armamos las listas de colaboradores por cada cluster
            var clusteredEmployees = new List<List<ViajesdetallesCreateClusteredDto>>();
            foreach (var cluster in clusters)
            {
                var group = employees
                    .Where((_, idx) => cluster.Contains(idx))
                    .ToList();

                if (group.Any())
                    clusteredEmployees.Add(group);
            }

            return clusteredEmployees;
        }

        /// <summary>
        /// Crea viajes a partir de los "clusters" generados. Aplica reglas de negocio:
        ///   - Sumatoria de kilómetros no > 100.
        ///   - Un colaborador no puede viajar más de una vez al día.
        ///   - Se verifica si hay un viaje abierto (estado=1004) en la misma sucursal para esa fecha.
        /// </summary>
        /// 
        //public List<Viajes> CreateTripsFromClusters(
        //    ViajesCreateClusteredDto tripDto,
        //    List<List<ViajesdetallesCreateClusteredDto>> clusteredEmployees,
        //    List<Viajes> viajesAbiertosMismaSucursal, // Viajes con estado=1004 en la sucursal y fecha
        //    Func<int, DateTime, bool> colaboradorYaViajaHoy) // Función para verificar si un colaborador ya viaja en esa fecha
        //{
        //    if (tripDto == null)
        //        throw new ArgumentNullException(nameof(tripDto));
        //    if (tripDto.SucursalId <= 0 || tripDto.EstadoId <= 0)
        //        throw new ArgumentException("Datos inválidos en la creación de viaje.");
        //    if (clusteredEmployees == null || !clusteredEmployees.Any())
        //        throw new ArgumentException("No hay clusters de colaboradores para crear viajes.");
        //    if (tripDto.TransportistaIds == null || tripDto.TransportistaIds.Count < clusteredEmployees.Count)
        //        throw new ArgumentException("No hay suficientes transportistas para asignar a los clusters.");

        //    var trips = new List<Viajes>();
        //    var availableTransportistas = new List<int>(tripDto.TransportistaIds);

        //    // Recorremos cada cluster y creamos (o asignamos) un viaje
        //    foreach (var cluster in clusteredEmployees)
        //    {
        //        // Validación: que ninguno de estos colaboradores ya tenga viaje ese día
        //        foreach (var colab in cluster)
        //        {
        //            if (colaboradorYaViajaHoy(colab.ColaboradorId, tripDto.Viajefecha))
        //                throw new Exception($"El colaborador {colab.ColaboradorId} ya tiene viaje en la fecha {tripDto.Viajefecha:yyyy-MM-dd}.");
        //        }

        //        // Armamos el detalle
        //        var tripDetails = cluster.Select(d => new Viajesdetalles
        //        {
        //            ColaboradorId = d.ColaboradorId,
        //            Distanciakilometros = d.Distanciakilometros,
        //            Totalpagar = d.Totalpagar,
        //            ColaboradorsucursalId = d.ColaboradorsucursalId,
        //            Activo = true,
        //            Usuariocrea = tripDto.Usuariocrea,
        //            Fechacrea = DateTime.Now,
        //            MonedaId = d.MonedaId
        //        }).ToList();

        //        var sumDist = tripDetails.Sum(d => d.Distanciakilometros);
        //        if (sumDist > 100)
        //            throw new Exception($"Un viaje no puede acumular más de 100km. Se detectó {sumDist}km en uno de los clusters.");

        //        // Validación: vemos si existe un viaje abierto (estado=1004) que podamos usar
        //        // para "agregar" estos colaboradores, considerando la distancia con un umbral.
        //        // (Esto es un ejemplo simplificado: ajusta la lógica si deseas más criterios).
        //        var viajeAbierto = viajesAbiertosMismaSucursal.FirstOrDefault(v =>
        //            v.Viajefecha == tripDto.Viajefecha &&
        //            v.Totalkilometros + sumDist <= 100 // que no exceda 100 al sumarlos
        //        );

        //        if (viajeAbierto != null)
        //        {
        //            // Agregamos detalles al viaje existente
        //            viajeAbierto.Totalkilometros += sumDist;
        //            viajeAbierto.Totalpagar += tripDetails.Sum(d => d.Totalpagar);
        //            foreach (var det in tripDetails)
        //            {
        //                det.ViajeId = viajeAbierto.ViajeId;
        //                viajeAbierto.Viajesdetalles.Add(det);
        //            }
        //        }
        //        else
        //        {
        //            // Creamos un viaje nuevo y asignamos un transportista de la lista disponible
        //            int randomIndex = _random.Next(0, availableTransportistas.Count);
        //            int transportistaId = availableTransportistas[randomIndex];
        //            availableTransportistas.RemoveAt(randomIndex);

        //            var nuevoViaje = new Viajes
        //            {
        //                SucursalId = tripDto.SucursalId,
        //                TransportistaId = transportistaId,
        //                EstadoId = tripDto.EstadoId,
        //                Viajehora = tripDto.Viajehora,
        //                Viajefecha = tripDto.Viajefecha,
        //                Totalkilometros = sumDist,
        //                Totalpagar = tripDetails.Sum(d => d.Totalpagar),
        //                Activo = true,
        //                Usuariocrea = tripDto.Usuariocrea,
        //                Fechacrea = DateTime.Now,
        //                MonedaId = tripDto.MonedaId,
        //                // ¡Importante! Utiliza ICollection<Viajesdetalles> en la entidad
        //                Viajesdetalles = tripDetails
        //            };
        //            trips.Add(nuevoViaje);
        //        }
        //    }

        //    // Devolvemos la lista de viajes nuevos.
        //    // OJO: Si se añadieron colaboradores a viajes abiertos, están dentro de `viajesAbiertosMismaSucursal`.
        //    // Podrías unificarlos si lo deseas.
        //    return trips;
        //}
        public List<Viajes> CreateTripsFromClusters(
           ViajesCreateClusteredDto tripDto,
           List<List<ViajesdetallesCreateClusteredDto>> clusteredEmployees,
           List<Viajes> viajesAbiertosMismaSucursal,
           Func<int, DateTime, bool> colaboradorYaViajaHoy,
           decimal distanceThreshold)
        {
            if (tripDto == null) throw new ArgumentNullException(nameof(tripDto));
            if (clusteredEmployees == null || !clusteredEmployees.Any())
                throw new ArgumentException("No hay clusters de colaboradores para crear viajes.");

            var trips = new List<Viajes>();
            var availableTransportistas = new List<int>(tripDto.TransportistaIds);

            foreach (var cluster in clusteredEmployees)
            {
                // 1. Validar que ningún colaborador viaje 2 veces el mismo día
                //foreach (var colab in cluster)
                //{
                //    if (colaboradorYaViajaHoy(colab.ColaboradorId, tripDto.Viajefecha))
                //        throw new Exception($"El colaborador {colab.ColaboradorId} ya tiene viaje en {tripDto.Viajefecha:yyyy-MM-dd}.");
                //}

                // 2. Construir detalles
                var tripDetails = cluster.Select(d => new Viajesdetalles
                {
                    ColaboradorId = d.ColaboradorId,
                    Distanciakilometros = d.Distanciakilometros,
                    Totalpagar = d.Totalpagar,
                    ColaboradorsucursalId = d.ColaboradorsucursalId,
                    Activo = true,
                    Usuariocrea = tripDto.Usuariocrea,
                    Fechacrea = DateTime.Now,
                    MonedaId = d.MonedaId
                }).ToList();

                // 3. Calcular la suma a pagar y la distancia máxima
                var sumPay = tripDetails.Sum(x => x.Totalpagar);
                var maxDist = tripDetails.Max(x => x.Distanciakilometros);

                // 4. Intentar unirnos a un viaje abierto (si está “cerca” según Google)
                //    Recorremos cada viajeAbierto y usamos la API de Google para comparar distancias.
                //    Podríamos quedarnos con el primero que cumpla la condición, o con el más cercano.
                Viajes viajeElegido = null;
                foreach (var vAbierto in viajesAbiertosMismaSucursal)
                {
                    // ¿Está cerca según la API de Google?
                    if (IsClusterCloseToViajeAbiertoGoogle(cluster, vAbierto, distanceThreshold))
                    {
                        viajeElegido = vAbierto;
                        break;
                    }
                }

                // 5. Si encontramos un viaje cercano, se añaden los detalles
                if (viajeElegido != null)
                {
                    // Actualizar totalkilometros = max de lo que tenía y lo nuevo
                    viajeElegido.Totalkilometros = Math.Max(viajeElegido.Totalkilometros, maxDist);
                    // Sumar pago
                    viajeElegido.Totalpagar += sumPay;

                    // Agregar los detalles
                    foreach (var det in tripDetails)
                    {
                        det.ViajeId = viajeElegido.ViajeId;
                        viajeElegido.Viajesdetalles.Add(det);
                    }
                }
                else
                {
                    // Sino, creamos un viaje nuevo
                    if (!availableTransportistas.Any())
                        throw new Exception("No hay más transportistas disponibles.");

                    int idx = _random.Next(0, availableTransportistas.Count);
                    int transportistaId = availableTransportistas[idx];
                    availableTransportistas.RemoveAt(idx);

                    var nuevoViaje = new Viajes
                    {
                        SucursalId = tripDto.SucursalId,
                        TransportistaId = transportistaId,
                        EstadoId = tripDto.EstadoId,
                        Viajehora = tripDto.Viajehora,
                        Viajefecha = tripDto.Viajefecha,
                        Totalkilometros = maxDist,
                        Totalpagar = sumPay,
                        Activo = true,
                        Usuariocrea = tripDto.Usuariocrea,
                        Fechacrea = DateTime.Now,
                        MonedaId = tripDto.MonedaId,
                        Viajesdetalles = tripDetails
                    };
                    trips.Add(nuevoViaje);
                }
            }

            return trips;
        }

        /// <summary>
        /// Verifica si "cluster" está dentro del umbral de distancia con el "viajeAbierto"
        /// usando la API de Google (distancematrix).
        /// </summary>
        private bool IsClusterCloseToViajeAbiertoGoogle(
            List<ViajesdetallesCreateClusteredDto> cluster,
            Viajes viajeAbierto,
            decimal distanceThreshold)
        {
            // 1. Obtener la lista de ubicaciones (lat,lon) de los colaboradores del viajeAbierto
            //    y de los colaboradores del cluster.
            //    OJO: hay que obtener la lat/lon de cada detalle. Si no está en Viajesdetalles,
            //    debemos traerlo desde "Colaboradorsucursal" o "Colaboradores".
            //    Asumimos que "vAbierto.Viajesdetalles" ya tiene "Colaboradorsucursal.Latitud/Longitud"
            var tripLocations = new List<string>();
            foreach (var det in viajeAbierto.Viajesdetalles)
            {
                // Ejemplo: si has incluído a "Colaboradorsucursal"
                var lat = det.Colaboradorsucursal?.Colaborador?.Latitud ?? 0; // Ajusta
                var lon = det.Colaboradorsucursal?.Colaborador?.Longitud ?? 0; // Ajusta
                tripLocations.Add($"{lat},{lon}");
            }

            // 2. Obtener ubicaciones del cluster
            var clusterLocations = cluster
                .Select(c => $"{c.Latitud},{c.Longitud}")
                .ToList();

            // 3. Unirlas en una sola lista para la Distance Matrix
            //    Queremos una NxN donde N = tripLocations.Count + clusterLocations.Count
            var allLocations = tripLocations.Concat(clusterLocations).ToList();

            // 4. Llamamos a la API de Google para obtener la matriz
            //    Este método ya existe en tu GoogleMapsService
            var distanceMatrix = _googleMapsService.GetDistanceMatrixAsync(allLocations).Result;
            // O hazlo 'async' y marca el método contenedor como async si prefieres.

            // 5. Interpretar la submatriz: 
            //    - Filas 0..(tripLocations.Count-1) corresponden a "viajeAbierto"
            //    - Filas tripLocations.Count..(allLocations.Count-1) corresponden al cluster
            //    (y lo mismo con las columnas, pues es NxN).
            int tripCount = tripLocations.Count;
            int clusterCount = clusterLocations.Count;

            // Decidimos la métrica. Por ejemplo:
            //   "Al menos un colaborador del cluster está a <= threshold km
            //    de al menos un colaborador del viaje."
            //   O "Todos los del cluster deben estar a <= threshold de alguien en el viaje."
            //   Ajusta a tu negocio.

            // Ejemplo: exijamos que TODOS los colaboradores del cluster
            // tienen que estar a <= threshold de AL MENOS UN colaborador del viaje.
            for (int i = 0; i < clusterCount; i++)
            {
                bool thisCollaboratorIsClose = false;
                int clusterRowIndex = tripCount + i; // fila en la matriz

                // Revisamos la distancia con cada colaborador del viaje:
                for (int j = 0; j < tripCount; j++)
                {
                    double distKm = distanceMatrix[clusterRowIndex, j];
                    if (distKm <= (double)distanceThreshold)
                    {
                        thisCollaboratorIsClose = true;
                        break; // no necesitamos seguir
                    }
                }

                if (!thisCollaboratorIsClose)
                {
                    // Un colaborador del cluster no está cerca de nadie en el viaje => false
                    return false;
                }
            }

            // Si llegamos aquí, es que TODOS los colaboradores del cluster
            // están cerca de al menos un colaborador del viaje:
            return true;
        }
        // ------------------------------------------------------------------------------------
        // Métodos de apoyo (clustering). Se mantienen igual que antes, salvo que quitamos la llamada a Google.
        // ------------------------------------------------------------------------------------

        private List<List<int>> HierarchicalClustering(double[,] distances, double maxDistance)
        {
            int n = distances.GetLength(0);
            var clusters = Enumerable.Range(0, n).Select(i => new List<int> { i }).ToList();
            var minDistances = new List<(int cluster1, int cluster2, double distance)>();

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    minDistances.Add((i, j, distances[i, j]));
                }
            }

            while (minDistances.Any())
            {
                var closest = minDistances.OrderBy(d => d.distance).First();
                if (closest.distance > maxDistance)
                    break;

                if (closest.cluster1 >= clusters.Count || closest.cluster2 >= clusters.Count)
                {
                    Console.WriteLine($"Índice fuera de rango: cluster1={closest.cluster1}, cluster2={closest.cluster2}, clusters.Count={clusters.Count}");
                    break;
                }

                var cluster1 = clusters[closest.cluster1];
                var cluster2 = clusters[closest.cluster2];

                cluster1.AddRange(cluster2);
                clusters.RemoveAt(closest.cluster2);

                var newMinDistances = new List<(int cluster1, int cluster2, double distance)>();
                for (int i = 0; i < clusters.Count; i++)
                {
                    for (int j = i + 1; j < clusters.Count; j++)
                    {
                        double minDist = double.MaxValue;
                        foreach (var idx1 in clusters[i])
                        {
                            foreach (var idx2 in clusters[j])
                            {
                                if (idx1 >= distances.GetLength(0) || idx2 >= distances.GetLength(0))
                                    throw new IndexOutOfRangeException($"Índice inválido en distances: idx1={idx1}, idx2={idx2}, tamaño={distances.GetLength(0)}");
                                minDist = Math.Min(minDist, distances[idx1, idx2]);
                            }
                        }
                        newMinDistances.Add((i, j, minDist));
                    }
                }
                minDistances = newMinDistances;
            }

            return clusters;
        }
    }
}
