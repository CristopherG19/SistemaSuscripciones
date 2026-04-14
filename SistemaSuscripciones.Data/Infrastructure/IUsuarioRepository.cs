using SistemaSuscripciones.Entities;
using System.Collections.Generic;

namespace SistemaSuscripciones.Data.Infrastructure
{
    public interface IUsuarioRepository
    {
        Usuario ValidarLogin(string correo, string clave);
        int RegistrarUsuario(Usuario usuario);
        List<Usuario> ListarTodo();
        Usuario ObtenerPorId(int id);
        void Editar(Usuario usuario);
        void CambiarEstado(int id, bool activo);
    }
}