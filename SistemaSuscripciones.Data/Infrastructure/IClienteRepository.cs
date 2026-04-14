using SistemaSuscripciones.Entities;
using System.Collections.Generic;

namespace SistemaSuscripciones.Data.Infrastructure
{
    public interface IClienteRepository
    {
        void Registrar(Cliente entidad);
        List<Cliente> ListarTodo();
        Cliente ObtenerPorId(int id);
        void Editar(Cliente entidad);
        void Eliminar(int id);
    }
}
