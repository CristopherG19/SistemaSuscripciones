using SistemaSuscripciones.Entities;
using SistemaSuscripciones.Data.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace SistemaSuscripciones.Data.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly string _cadenaConexion;

        public DashboardRepository(IConfiguration config)
        {
            _cadenaConexion = config.GetConnectionString("ConexionSQL");
        }

        public ResumenDashboard ObtenerResumen()
        {
            ResumenDashboard resumen = new ResumenDashboard();
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                string query = "sp_ObtenerResumenDashboard";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            resumen.TotalClientes = Convert.ToInt32(reader["TotalClientes"]);
                            resumen.SuscripcionesActivas = Convert.ToInt32(reader["SuscripcionesActivas"]);
                            resumen.SuscripcionesCanceladas = Convert.ToInt32(reader["SuscripcionesCanceladas"]);
                            resumen.IngresosMensuales = Convert.ToDecimal(reader["IngresosMensuales"]);
                        }
                    }
                }
            }
            return resumen;
        }

        public List<Suscripcion> ObtenerSuscripcionesPorVencer()
        {
            List<Suscripcion> lista = new List<Suscripcion>();
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                string query = "sp_ObtenerSuscripcionesPorVencer";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Suscripcion
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                ClienteNombre = reader["ClienteNombre"].ToString(),
                                DocumentoIdentidad = reader["DocumentoIdentidad"].ToString(),
                                PlanId = Convert.ToInt32(reader["PlanId"]),
                                FechaInicio = Convert.ToDateTime(reader["FechaInicio"]),
                                FechaFin = Convert.ToDateTime(reader["FechaFin"]),
                                Estado = reader["Estado"].ToString(),
                                NombrePlan = reader["NombrePlan"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public List<DistribucionPlan> ObtenerDistribucionPorPlan()
        {
            List<DistribucionPlan> lista = new List<DistribucionPlan>();
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                string query = "sp_ObtenerDistribucionPorPlan";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new DistribucionPlan
                            {
                                NombrePlan = reader["NombrePlan"].ToString(),
                                CantidadSuscripciones = Convert.ToInt32(reader["CantidadSuscripciones"])
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}
