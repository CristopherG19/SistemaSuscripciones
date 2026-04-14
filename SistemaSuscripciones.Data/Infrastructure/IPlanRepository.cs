using SistemaSuscripciones.Entities;
using System.Collections.Generic;

namespace SistemaSuscripciones.Data.Infrastructure
{
    public interface IPlanRepository
    {
        List<Plan> ListarTodo();
        Plan ObtenerPorId(int id);
        void Registrar(Plan entidad);
        void Editar(Plan entidad);
        void Eliminar(int id);
    }
}
