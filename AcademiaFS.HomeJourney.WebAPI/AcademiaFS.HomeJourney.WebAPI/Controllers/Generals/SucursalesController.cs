using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using AcademiaFS.HomeJourney.WebAPI.Utilities;

namespace AcademiaFS.HomeJourney.WebAPI.Controllers.Generals
{
    [ApiController]
    [Route("academiafarsiman/sucursales")]
    public class SucursalesController : Controller
    {
        private readonly string _connectionString = "Data Source=localhost,1433;Initial Catalog=HomeJourney;User ID=sa;Password=TuContraseñaSegura123;Encrypt=True;TrustServerCertificate=True;";
        //private readonly string _connectionString = "Data Source=192.168.1.33\\academiagfs,49194;Initial Catalog=HomeJourney;User ID=AcademiaDEV;Password=Academia.1;Encrypt=True;TrustServerCertificate=True;";

        [HttpGet()]
        public async Task<ActionResult<CustomResponse<IEnumerable<Sucursales>>>> GetAllRaw()
        {
            var sucursales = new List<Sucursales>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string query = @"SELECT Sucursal_id, Nombre, Direccion, Activo, 
                                            Usuariocrea, Fechacrea, Usuariomodifica, 
                                            Fechamodifica, Latitud, Longitud,
                                            Jefe_Id
                                     FROM Sucursales";
                                            //--JefeId
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var sucursal = new Sucursales
                                {
                                    SucursalId = reader.GetInt32(reader.GetOrdinal("Sucursal_id")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    Direccion = reader.GetString(reader.GetOrdinal("Direccion")),
                                    Activo = reader.GetBoolean(reader.GetOrdinal("Activo")),
                                    Usuariocrea = reader.GetInt32(reader.GetOrdinal("Usuariocrea")),
                                    Fechacrea = reader.GetDateTime(reader.GetOrdinal("Fechacrea")),
                                    Usuariomodifica = reader.IsDBNull(reader.GetOrdinal("Usuariomodifica"))
                                                        ? (int?)null
                                                        : reader.GetInt32(reader.GetOrdinal("Usuariomodifica")),
                                    Fechamodifica = reader.IsDBNull(reader.GetOrdinal("Fechamodifica"))
                                                        ? (DateTime?)null
                                                        : reader.GetDateTime(reader.GetOrdinal("Fechamodifica")),
                                    Latitud = reader.GetDecimal(reader.GetOrdinal("Latitud")),
                                    Longitud = reader.GetDecimal(reader.GetOrdinal("Longitud")),
                                    JefeId = reader.IsDBNull(reader.GetOrdinal("Jefe_Id"))
                                                        ? (int?)null
                                                        : reader.GetInt32(reader.GetOrdinal("Jefe_Id"))
                                    //JefeId = reader.IsDBNull(reader.GetOrdinal("JefeId"))
                                    //                    ? (int?)null
                                    //                    : reader.GetInt32(reader.GetOrdinal("JefeId"))
                                };

                                sucursales.Add(sucursal);
                            }
                        }
                    }
                }

                var response = new CustomResponse<IEnumerable<Sucursales>>
                {
                    Success = true,
                    Message = "Listado de sucursales obtenido correctamente",
                    Data = sucursales
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CustomResponse<string>
                {
                    Success = false,
                    Message = $"Ocurrió un error al obtener las sucursales: {ex.Message}"
                });
            }
        }
    }
}
