using SistemaSuscripciones.Entities;

namespace SistemaSuscripciones.Data.Infrastructure
{
    public interface IUsuarioRepository
    {
        Usuario ValidarLogin(string correo, string clave);
    }
}