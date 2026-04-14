using SistemaSuscripciones.Entities;
using SistemaSuscripciones.Data.Infrastructure;
using System;
using System.Collections.Generic;

namespace SistemaSuscripciones.BusinessLogic
{
    public class PagoService : IPagoService
    {
        private readonly IPagoRepository _repositorio;

        public PagoService(IPagoRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public void RegistrarPago(Pago pago)
        {
            if (pago.SuscripcionId <= 0)
                throw new Exception("Debe seleccionar una suscripción válida.");

            if (pago.Monto <= 0)
                throw new Exception("El monto del pago debe ser mayor a cero.");

            if (string.IsNullOrWhiteSpace(pago.MetodoPago))
                throw new Exception("Debe indicar el método de pago.");

            // Si no se indica fecha, se usa la actual
            if (pago.FechaPago == DateTime.MinValue)
                pago.FechaPago = DateTime.Now;

            _repositorio.Registrar(pago);
        }

        public List<Pago> ObtenerPagos()
        {
            return _repositorio.ListarTodo();
        }

        public List<Pago> ObtenerPagosPorSuscripcion(int suscripcionId)
        {
            return _repositorio.ListarPorSuscripcion(suscripcionId);
        }

        public Pago ObtenerPago(int id)
        {
            return _repositorio.ObtenerPorId(id);
        }
    }
}
