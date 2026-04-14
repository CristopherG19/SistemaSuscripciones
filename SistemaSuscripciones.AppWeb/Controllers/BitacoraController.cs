using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SistemaSuscripciones.BusinessLogic;
using System;

namespace SistemaSuscripciones.AppWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BitacoraController : Controller
    {
        private readonly IBitacoraService _servicio;

        public BitacoraController(IBitacoraService servicio)
        {
            _servicio = servicio;
        }

        // GET: Listado de registros de bitácora con filtros opcionales
        public IActionResult Index(DateTime? fechaDesde, DateTime? fechaHasta)
        {
            var modelo = _servicio.ObtenerRegistros(fechaDesde, fechaHasta);

            // Preservar los filtros en la vista
            ViewBag.FechaDesde = fechaDesde?.ToString("yyyy-MM-dd");
            ViewBag.FechaHasta = fechaHasta?.ToString("yyyy-MM-dd");

            return View(modelo);
        }
    }
}
