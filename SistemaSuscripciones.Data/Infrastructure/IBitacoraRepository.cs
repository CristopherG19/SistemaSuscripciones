using SistemaSuscripciones.Entities;
using System.Collections.Generic;
using System;

namespace SistemaSuscripciones.Data.Infrastructure
{
    public interface IBitacoraRepository
    {
        void Registrar(string nombreUsuario, string accion, string detalle);
        List<Bitacora> Listar(DateTime? fechaDesde, DateTime? fechaHasta);
    }
}
