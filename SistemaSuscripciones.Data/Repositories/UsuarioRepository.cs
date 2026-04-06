using SistemaSuscripciones.Entities;
using SistemaSuscripciones.Data.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;

namespace SistemaSuscripciones.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _cadenaConexion;

        public UsuarioRepository(IConfiguration config)
        {
            _cadenaConexion = config.GetConnectionString("ConexionSQL");
        }

        public Usuario ValidarLogin(string correo, string clave)
        {
            Usuario usuario = null;

            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                string query = "sp_ValidarUsuario";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Correo", correo);
                    cmd.Parameters.AddWithValue("@Clave", clave);

                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // Si encontró al usuario
                        {
                            usuario = new Usuario
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                NombreCompleto = reader["NombreCompleto"].ToString(),
                                Correo = reader["Correo"].ToString(),
                                Rol = reader["Rol"].ToString()
                            };
                        }
                    }
                }
            }
            return usuario; // Si no lo encuentra, devuelve null
        }
    }
}