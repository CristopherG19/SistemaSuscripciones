using SistemaSuscripciones.Entities;
using SistemaSuscripciones.Data.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace SistemaSuscripciones.Data.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly string _cadenaConexion;

        public ClienteRepository(IConfiguration config)
        {
            _cadenaConexion = config.GetConnectionString("ConexionSQL");
        }

        public void Registrar(Cliente entidad)
        {
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                string query = "sp_RegistrarCliente";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Nombres", entidad.Nombres);
                    cmd.Parameters.AddWithValue("@Apellidos", entidad.Apellidos);
                    cmd.Parameters.AddWithValue("@DocumentoIdentidad", entidad.DocumentoIdentidad);
                    cmd.Parameters.AddWithValue("@Telefono", (object)entidad.Telefono ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Correo", (object)entidad.Correo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Direccion", (object)entidad.Direccion ?? DBNull.Value);

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Cliente> ListarTodo()
        {
            List<Cliente> lista = new List<Cliente>();
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                string query = "sp_ListarClientes";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Cliente
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nombres = reader["Nombres"].ToString(),
                                Apellidos = reader["Apellidos"].ToString(),
                                DocumentoIdentidad = reader["DocumentoIdentidad"].ToString(),
                                Telefono = reader["Telefono"] != DBNull.Value ? reader["Telefono"].ToString() : "",
                                Correo = reader["Correo"] != DBNull.Value ? reader["Correo"].ToString() : "",
                                Direccion = reader["Direccion"] != DBNull.Value ? reader["Direccion"].ToString() : "",
                                FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"]),
                                Activo = Convert.ToBoolean(reader["Activo"])
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public Cliente ObtenerPorId(int id)
        {
            Cliente cliente = null;
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                string query = "sp_ObtenerClientePorId";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cliente = new Cliente
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nombres = reader["Nombres"].ToString(),
                                Apellidos = reader["Apellidos"].ToString(),
                                DocumentoIdentidad = reader["DocumentoIdentidad"].ToString(),
                                Telefono = reader["Telefono"] != DBNull.Value ? reader["Telefono"].ToString() : "",
                                Correo = reader["Correo"] != DBNull.Value ? reader["Correo"].ToString() : "",
                                Direccion = reader["Direccion"] != DBNull.Value ? reader["Direccion"].ToString() : "",
                                FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"]),
                                Activo = Convert.ToBoolean(reader["Activo"])
                            };
                        }
                    }
                }
            }
            return cliente;
        }

        public void Editar(Cliente entidad)
        {
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                string query = "sp_EditarCliente";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", entidad.Id);
                    cmd.Parameters.AddWithValue("@Nombres", entidad.Nombres);
                    cmd.Parameters.AddWithValue("@Apellidos", entidad.Apellidos);
                    cmd.Parameters.AddWithValue("@DocumentoIdentidad", entidad.DocumentoIdentidad);
                    cmd.Parameters.AddWithValue("@Telefono", (object)entidad.Telefono ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Correo", (object)entidad.Correo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Direccion", (object)entidad.Direccion ?? DBNull.Value);

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Eliminar(int id)
        {
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                string query = "sp_EliminarCliente";

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
