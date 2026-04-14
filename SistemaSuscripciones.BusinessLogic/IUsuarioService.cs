using SistemaSuscripciones.Entities;
using System.Collections.Generic;

namespace SistemaSuscripciones.BusinessLogic
{
    public interface IUsuarioService
    {
        Usuario Autenticar(string correo, string clave);
        bool Registrar(Usuario usuario);
        List<Usuario> ObtenerUsuarios();
        Usuario ObtenerUsuario(int id);
        void EditarUsuario(Usuario usuario);
        void CambiarEstadoUsuario(int id, bool activo);
    }
}