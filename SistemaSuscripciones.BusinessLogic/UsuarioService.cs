using SistemaSuscripciones.Entities;
using SistemaSuscripciones.Data.Infrastructure;

namespace SistemaSuscripciones.BusinessLogic
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repositorio;

        public UsuarioService(IUsuarioRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public Usuario Autenticar(string correo, string clave)
        {
            // Aquí podrías agregar lógica extra (ej. encriptar la clave antes de enviarla a la BD)
            return _repositorio.ValidarLogin(correo, clave);
        }

        public bool Registrar(Usuario usuario)
        {
            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(usuario.Correo) || string.IsNullOrWhiteSpace(usuario.Clave) || string.IsNullOrWhiteSpace(usuario.NombreCompleto))
                return false;

            // Por defecto los que se registran son Empleados (Según plan aprobado)
            if(string.IsNullOrEmpty(usuario.Rol))
                usuario.Rol = "Empleado";

            // En un futuro es ideal encriptar la clave aquí
            
            int newId = _repositorio.RegistrarUsuario(usuario);
            return newId > 0;
        }
    }
}