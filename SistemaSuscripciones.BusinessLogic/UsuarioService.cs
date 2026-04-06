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
    }
}