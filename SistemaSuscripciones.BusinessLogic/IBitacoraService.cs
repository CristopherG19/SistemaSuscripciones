using SistemaSuscripciones.Entities;
using System;
using System.Collections.Generic;

namespace SistemaSuscripciones.BusinessLogic
{
    public interface IBitacoraService
    {
        void RegistrarAccion(string nombreUsuario, string accion, string detalle);
        List<Bitacora> ObtenerRegistros(DateTime? fechaDesde, DateTime? fechaHasta);
    }
}
