using System;
using System.Collections.Generic;
using System.Text;

using SistemaSuscripciones.Entities;
using System.Collections.Generic;

namespace SistemaSuscripciones.Data.Infrastructure
{
    public interface ISuscripcionRepository
    {
        void Registrar(Suscripcion entidad);
        List<Suscripcion> ListarTodo();
        int ObtenerDuracionPlan(int planId);

        // Nuevos métodos
        Suscripcion ObtenerPorId(int id);
        void Anular(int id);
        void Renovar(int id, int nuevoPlanId, DateTime nuevaFechaFin);
    }
}