using SistemaSuscripciones.Entities;
using SistemaSuscripciones.Data.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace SistemaSuscripciones.Data.Repositories
{
    public class BitacoraRepository : IBitacoraRepository
    {
        private readonly string _cadenaConexion;

        public BitacoraRepository(IConfiguration config)
        {
            _cadenaConexion = config.GetConnectionString("ConexionSQL");
        }

        public void Registrar(string nombreUsuario, string accion, string detalle)
        {
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                string query = "sp_RegistrarBitacora";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                    cmd.Parameters.AddWithValue("@Accion", accion);
                    cmd.Parameters.AddWithValue("@Detalle", (object)detalle ?? DBNull.Value);

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Bitacora> Listar(DateTime? fechaDesde, DateTime? fechaHasta)
        {
            List<Bitacora> lista = new List<Bitacora>();
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                string query = "sp_ListarBitacora";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FechaDesde", (object)fechaDesde ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@FechaHasta", (object)fechaHasta ?? DBNull.Value);

                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Bitacora
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                NombreUsuario = reader["NombreUsuario"].ToString(),
                                Accion = reader["Accion"].ToString(),
                                Detalle = reader["Detalle"] != DBNull.Value ? reader["Detalle"].ToString() : "",
                                FechaHora = Convert.ToDateTime(reader["FechaHora"])
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}
