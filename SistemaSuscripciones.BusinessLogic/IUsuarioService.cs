using SistemaSuscripciones.Entities;

namespace SistemaSuscripciones.BusinessLogic
{
    public interface IUsuarioService
    {
        Usuario Autenticar(string correo, string clave);
    }
}