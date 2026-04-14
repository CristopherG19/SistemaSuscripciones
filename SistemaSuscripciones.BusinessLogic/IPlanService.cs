using SistemaSuscripciones.Entities;
using System.Collections.Generic;

namespace SistemaSuscripciones.BusinessLogic
{
    public interface IPlanService
    {
        List<Plan> ObtenerPlanes();
        Plan ObtenerPlan(int id);
        void RegistrarPlan(Plan plan);
        void EditarPlan(Plan plan);
        void EliminarPlan(int id);
    }
}
