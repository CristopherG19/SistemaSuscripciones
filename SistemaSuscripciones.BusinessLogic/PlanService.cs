using SistemaSuscripciones.Entities;
using SistemaSuscripciones.Data.Infrastructure;
using System;
using System.Collections.Generic;

namespace SistemaSuscripciones.BusinessLogic
{
    public class PlanService : IPlanService
    {
        private readonly IPlanRepository _repositorio;

        public PlanService(IPlanRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public List<Plan> ObtenerPlanes()
        {
            return _repositorio.ListarTodo();
        }

        public Plan ObtenerPlan(int id)
        {
            return _repositorio.ObtenerPorId(id);
        }

        public void RegistrarPlan(Plan plan)
        {
            if (string.IsNullOrWhiteSpace(plan.Nombre))
                throw new Exception("El nombre del plan es obligatorio.");

            if (plan.Precio <= 0)
                throw new Exception("El precio debe ser mayor a cero.");

            if (plan.DuracionMeses <= 0)
                throw new Exception("La duración en meses debe ser mayor a cero.");

            _repositorio.Registrar(plan);
        }

        public void EditarPlan(Plan plan)
        {
            if (string.IsNullOrWhiteSpace(plan.Nombre))
                throw new Exception("El nombre del plan es obligatorio.");

            if (plan.Precio <= 0)
                throw new Exception("El precio debe ser mayor a cero.");

            if (plan.DuracionMeses <= 0)
                throw new Exception("La duración en meses debe ser mayor a cero.");

            _repositorio.Editar(plan);
        }

        public void EliminarPlan(int id)
        {
            _repositorio.Eliminar(id);
        }
    }
}
