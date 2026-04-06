using Microsoft.AspNetCore.Mvc;
using SistemaSuscripciones.BusinessLogic;
using SistemaSuscripciones.Entities;
using Microsoft.AspNetCore.Authorization;

namespace SistemaSuscripciones.AppWeb.Controllers
{
    [Authorize]
    public class SuscripcionController : Controller
    {
        private readonly ISuscripcionService _servicio;

        public SuscripcionController(ISuscripcionService servicio)
        {
            _servicio = servicio;
        }

        public IActionResult Index()
        {
            var modelo = _servicio.ObtenerSuscripciones();
            return View(modelo);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Suscripcion suscripcion)
        {
            try
            {
                _servicio.GenerarSuscripcion(suscripcion);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(suscripcion);
            }
        }

        // POST: Anular suscripción
        [HttpPost]
        public IActionResult Anular(int id)
        {
            try
            {
                _servicio.AnularSuscripcion(id);
                TempData["Exito"] = "La suscripción fue anulada con éxito.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Hubo un problema al anular: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Muestra la pantalla de renovación
        public IActionResult Renovar(int id)
        {
            var suscripcion = _servicio.ObtenerSuscripcion(id);
            if (suscripcion == null)
            {
                return NotFound();
            }
            return View(suscripcion);
        }

        // POST: Procesa la renovación
        [HttpPost]
        public IActionResult Renovar(int Id, int PlanId)
        {
            try
            {
                _servicio.RenovarSuscripcion(Id, PlanId);
                TempData["Exito"] = "La suscripción se renovó correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Usamos TempData para enviar el error a la vista Index
                TempData["Error"] = "Hubo un problema al renovar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}