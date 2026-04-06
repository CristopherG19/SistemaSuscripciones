using SistemaSuscripciones.Entities;
using System.Collections.Generic;

namespace SistemaSuscripciones.BusinessLogic
{
    public interface ISuscripcionService
    {
        void GenerarSuscripcion(Suscripcion suscripcion);
        List<Suscripcion> ObtenerSuscripciones();
        Suscripcion ObtenerSuscripcion(int id);
        void AnularSuscripcion(int id);
        void RenovarSuscripcion(int suscripcionId, int nuevoPlanId);
    }
}