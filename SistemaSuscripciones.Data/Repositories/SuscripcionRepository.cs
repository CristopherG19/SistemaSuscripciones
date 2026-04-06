using System;
using System.Collections.Generic;
using System.Text;
using SistemaSuscripciones.Entities;
using SistemaSuscripciones.Data.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace SistemaSuscripciones.Data.Repositories
{
    public class SuscripcionRepository : ISuscripcionRepository
    {
        private readonly string _cadenaConexion;

        public SuscripcionRepository(IConfiguration config)
        {
            _cadenaConexion = config.GetConnectionString("ConexionSQL");
        }

        public void Registrar(Suscripcion entidad)
        {
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                // Solo se indica el nombre del procedimiento almacenado
                string query = "sp_RegistrarSuscripcion";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    // Se especifica que el comando es un procedimiento almacenado
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Los nombres de los parámetros deben coincidir con los del script SQL
                    cmd.Parameters.AddWithValue("@ClienteNombre", entidad.ClienteNombre);
                    cmd.Parameters.AddWithValue("@DocumentoIdentidad", entidad.DocumentoIdentidad);
                    cmd.Parameters.AddWithValue("@PlanId", entidad.PlanId);
                    cmd.Parameters.AddWithValue("@FechaInicio", entidad.FechaInicio);
                    cmd.Parameters.AddWithValue("@FechaFin", entidad.FechaFin);
                    cmd.Parameters.AddWithValue("@Estado", entidad.Estado);

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Suscripcion> ListarTodo()
        {
            List<Suscripcion> lista = new List<Suscripcion>();
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                // Nombre del procedimiento almacenado
                string query = "sp_ListarSuscripciones";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    // Se especifica que el comando es un procedimiento almacenado
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
        public int ObtenerDuracionPlan(int planId)
        {
            int duracion = 0;
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                string query = "sp_ObtenerDuracionPlan";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    // Especificamos al comando que debe ejecutar un procedimiento almacenado
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Pasamos el parámetro esperado por el procedimiento
                    cmd.Parameters.AddWithValue("@Id", planId);

                    conexion.Open();
                    duracion = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return duracion;
        }

        public Suscripcion ObtenerPorId(int id)
        {
            Suscripcion suscripcion = null;
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                string query = "sp_ObtenerSuscripcionPorId";
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            suscripcion = new Suscripcion
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                ClienteNombre = reader["ClienteNombre"].ToString(),
                                DocumentoIdentidad = reader["DocumentoIdentidad"].ToString(),
                                PlanId = Convert.ToInt32(reader["PlanId"]),
                                FechaInicio = Convert.ToDateTime(reader["FechaInicio"]),
                                FechaFin = Convert.ToDateTime(reader["FechaFin"]),
                                Estado = reader["Estado"].ToString(),
                                NombrePlan = reader["NombrePlan"].ToString()
                            };
                        }
                    }
                }
            }
            return suscripcion;
        }

        public void Anular(int id)
        {
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                string query = "sp_AnularSuscripcion";
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Renovar(int id, int nuevoPlanId, DateTime nuevaFechaFin)
        {
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                string query = "sp_RenovarSuscripcion";
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@NuevoPlanId", nuevoPlanId);
                    cmd.Parameters.AddWithValue("@NuevaFechaFin", nuevaFechaFin);
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}