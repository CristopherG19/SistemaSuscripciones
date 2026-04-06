using SistemaSuscripciones.Entities;
using SistemaSuscripciones.Data.Infrastructure;
using System;
using System.Collections.Generic;

namespace SistemaSuscripciones.BusinessLogic
{
    public class SuscripcionService : ISuscripcionService
    {
        private readonly ISuscripcionRepository _repositorio;

        public SuscripcionService(ISuscripcionRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public void GenerarSuscripcion(Suscripcion suscripcion)
        {
            // 1. Consultamos a la base de datos la duración real del plan seleccionado
            int duracionReal = _repositorio.ObtenerDuracionPlan(suscripcion.PlanId);

            // 2. Aplicamos las reglas de negocio
            suscripcion.FechaInicio = DateTime.Now;
            suscripcion.FechaFin = suscripcion.FechaInicio.AddMonths(duracionReal);
            suscripcion.Estado = "Activo";

            // 3. Guardamos
            _repositorio.Registrar(suscripcion);
        }

        public List<Suscripcion> ObtenerSuscripciones()
        {
            return _repositorio.ListarTodo();
        }

        public Suscripcion ObtenerSuscripcion(int id)
        {
            return _repositorio.ObtenerPorId(id);
        }

        public void AnularSuscripcion(int id)
        {
            _repositorio.Anular(id);
        }

        public void RenovarSuscripcion(int suscripcionId, int nuevoPlanId)
        {
            // 1. Obtener la suscripción actual y la duración del nuevo plan
            var suscripcionActual = _repositorio.ObtenerPorId(suscripcionId);
            int mesesDuracion = _repositorio.ObtenerDuracionPlan(nuevoPlanId);

            // 2. Lógica de negocio: Calcular la nueva fecha base
            // Si la suscripción aún no vence, agregamos tiempo a su fecha de fin.
            // Si ya venció, el tiempo empieza a correr desde hoy.
            DateTime fechaBaseCalculo = suscripcionActual.FechaFin > DateTime.Now
                                        ? suscripcionActual.FechaFin
                                        : DateTime.Now;

            DateTime nuevaFechaFin = fechaBaseCalculo.AddMonths(mesesDuracion);

            // 3. Enviar a la base de datos
            _repositorio.Renovar(suscripcionId, nuevoPlanId, nuevaFechaFin);
        }
    }
}