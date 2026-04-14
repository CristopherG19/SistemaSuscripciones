using SistemaSuscripciones.Entities;
using SistemaSuscripciones.Data.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace SistemaSuscripciones.Data.Repositories
{
    public class PlanRepository : IPlanRepository
    {
        private readonly string _cadenaConexion;

        public PlanRepository(IConfiguration config)
        {
            _cadenaConexion = config.GetConnectionString("ConexionSQL");
        }

        public List<Plan> ListarTodo()
        {
            List<Plan> lista = new List<Plan>();
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                string query = "sp_ListarPlanes";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Plan
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nombre = reader["Nombre"].ToString(),
                                Precio = Convert.ToDecimal(reader["Precio"]),
                                DuracionMeses = Convert.ToInt32(reader["DuracionMeses"])
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public Plan ObtenerPorId(int id)
        {
            Plan plan = null;
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                string query = "sp_ObtenerPlanPorId";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            plan = new Plan
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nombre = reader["Nombre"].ToString(),
                                Precio = Convert.ToDecimal(reader["Precio"]),
                                DuracionMeses = Convert.ToInt32(reader["DuracionMeses"])
                            };
                        }
                    }
                }
            }
            return plan;
        }

        public void Registrar(Plan entidad)
        {
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                string query = "sp_RegistrarPlan";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Nombre", entidad.Nombre);
                    cmd.Parameters.AddWithValue("@Precio", entidad.Precio);
                    cmd.Parameters.AddWithValue("@DuracionMeses", entidad.DuracionMeses);

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Editar(Plan entidad)
        {
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                string query = "sp_EditarPlan";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", entidad.Id);
                    cmd.Parameters.AddWithValue("@Nombre", entidad.Nombre);
                    cmd.Parameters.AddWithValue("@Precio", entidad.Precio);
                    cmd.Parameters.AddWithValue("@DuracionMeses", entidad.DuracionMeses);

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Eliminar(int id)
        {
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                string query = "sp_EliminarPlan";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
