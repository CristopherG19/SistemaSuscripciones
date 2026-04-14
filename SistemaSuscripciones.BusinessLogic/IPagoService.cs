using SistemaSuscripciones.Entities;
using System.Collections.Generic;

namespace SistemaSuscripciones.BusinessLogic
{
    public interface IPagoService
    {
        void RegistrarPago(Pago pago);
        List<Pago> ObtenerPagos();
        List<Pago> ObtenerPagosPorSuscripcion(int suscripcionId);
        Pago ObtenerPago(int id);
    }
}
