using SistemaSuscripciones.Entities;
using SistemaSuscripciones.Data.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace SistemaSuscripciones.Data.Repositories
{
    public class PagoRepository : IPagoRepository
    {
        private readonly string _cadenaConexion;

        public PagoRepository(IConfiguration config)
        {
            _cadenaConexion = config.GetConnectionString("ConexionSQL");
        }

        public void Registrar(Pago entidad)
        {
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                string query = "sp_RegistrarPago";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SuscripcionId", entidad.SuscripcionId);
                    cmd.Parameters.AddWithValue("@Monto", entidad.Monto);
                    cmd.Parameters.AddWithValue("@FechaPago", entidad.FechaPago);
                    cmd.Parameters.AddWithValue("@MetodoPago", entidad.MetodoPago);
                    cmd.Parameters.AddWithValue("@Referencia", (object)entidad.Referencia ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Observaciones", (object)entidad.Observaciones ?? DBNull.Value);

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Pago> ListarTodo()
        {
            List<Pago> lista = new List<Pago>();
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                string query = "sp_ListarPagos";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(MapearPago(reader));
                        }
                    }
                }
            }
            return lista;
        }

        public List<Pago> ListarPorSuscripcion(int suscripcionId)
        {
            List<Pago> lista = new List<Pago>();
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                string query = "sp_ListarPagosPorSuscripcion";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SuscripcionId", suscripcionId);

                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(MapearPago(reader));
                        }
                    }
                }
            }
            return lista;
        }

        public Pago ObtenerPorId(int id)
        {
            Pago pago = null;
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                string query = "sp_ObtenerPagoPorId";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pago = MapearPago(reader);
                        }
                    }
                }
            }
            return pago;
        }

        // Método privado para evitar duplicar el mapeo de columnas
        private Pago MapearPago(SqlDataReader reader)
        {
            return new Pago
            {
                Id = Convert.ToInt32(reader["Id"]),
                SuscripcionId = Convert.ToInt32(reader["SuscripcionId"]),
                Monto = Convert.ToDecimal(reader["Monto"]),
                FechaPago = Convert.ToDateTime(reader["FechaPago"]),
                MetodoPago = reader["MetodoPago"].ToString(),
                Referencia = reader["Referencia"] != DBNull.Value ? reader["Referencia"].ToString() : "",
                Observaciones = reader["Observaciones"] != DBNull.Value ? reader["Observaciones"].ToString() : "",
                ClienteNombre = reader["ClienteNombre"].ToString(),
                NombrePlan = reader["NombrePlan"].ToString()
            };
        }
    }
}
