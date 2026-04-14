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
        private readonly IPlanService _planServicio;
        private readonly IBitacoraService _bitacoraServicio;

        public SuscripcionController(ISuscripcionService servicio, IPlanService planServicio, IBitacoraService bitacoraServicio)
        {
            _servicio = servicio;
            _planServicio = planServicio;
            _bitacoraServicio = bitacoraServicio;
        }

        public IActionResult Index()
        {
            var modelo = _servicio.ObtenerSuscripciones();
            return View(modelo);
        }

        public IActionResult Crear()
        {
            ViewBag.Planes = _planServicio.ObtenerPlanes();
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Suscripcion suscripcion)
        {
            try
            {
                _servicio.GenerarSuscripcion(suscripcion);
                _bitacoraServicio.RegistrarAccion(User.Identity.Name, "Registró suscripción", $"Cliente: {suscripcion.ClienteNombre}, Plan ID: {suscripcion.PlanId}");
                TempData["Exito"] = "La suscripción fue registrada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al registrar: " + ex.Message;
                ViewBag.Planes = _planServicio.ObtenerPlanes();
                return View(suscripcion);
            }
        }

        [HttpPost]
        public IActionResult Anular(int id)
        {
            try
            {
                _servicio.AnularSuscripcion(id);
                _bitacoraServicio.RegistrarAccion(User.Identity.Name, "Anuló suscripción", $"Suscripción ID: {id}");
                TempData["Exito"] = "La suscripción fue anulada con éxito.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Hubo un problema al anular: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Renovar(int id)
        {
            var suscripcion = _servicio.ObtenerSuscripcion(id);
            if (suscripcion == null)
            {
                return NotFound();
            }
            ViewBag.Planes = _planServicio.ObtenerPlanes();
            return View(suscripcion);
        }

        [HttpPost]
        public IActionResult Renovar(int Id, int PlanId)
        {
            try
            {
                _servicio.RenovarSuscripcion(Id, PlanId);
                _bitacoraServicio.RegistrarAccion(User.Identity.Name, "Renovó suscripción", $"Suscripción ID: {Id}, Nuevo Plan ID: {PlanId}");
                TempData["Exito"] = "La suscripción se renovó correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Hubo un problema al renovar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}