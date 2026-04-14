using SistemaSuscripciones.Entities;
using SistemaSuscripciones.Data.Infrastructure;
using System;
using System.Collections.Generic;

namespace SistemaSuscripciones.BusinessLogic
{
    public class BitacoraService : IBitacoraService
    {
        private readonly IBitacoraRepository _repositorio;

        public BitacoraService(IBitacoraRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public void RegistrarAccion(string nombreUsuario, string accion, string detalle)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                nombreUsuario = "Sistema";

            _repositorio.Registrar(nombreUsuario, accion, detalle);
        }

        public List<Bitacora> ObtenerRegistros(DateTime? fechaDesde, DateTime? fechaHasta)
        {
            return _repositorio.Listar(fechaDesde, fechaHasta);
        }
    }
}
