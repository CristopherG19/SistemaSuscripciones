using SistemaSuscripciones.Entities;
using System.Collections.Generic;

namespace SistemaSuscripciones.Data.Infrastructure
{
    public interface IPagoRepository
    {
        void Registrar(Pago entidad);
        List<Pago> ListarTodo();
        List<Pago> ListarPorSuscripcion(int suscripcionId);
        Pago ObtenerPorId(int id);
    }
}
