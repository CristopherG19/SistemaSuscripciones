using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SistemaSuscripciones.BusinessLogic;
using SistemaSuscripciones.Entities;

namespace SistemaSuscripciones.AppWeb.Controllers
{
    [Authorize]
    public class PagoController : Controller
    {
        private readonly IPagoService _servicio;
        private readonly ISuscripcionService _suscripcionServicio;
        private readonly IBitacoraService _bitacoraServicio;

        public PagoController(IPagoService servicio, ISuscripcionService suscripcionServicio, IBitacoraService bitacoraServicio)
        {
            _servicio = servicio;
            _suscripcionServicio = suscripcionServicio;
            _bitacoraServicio = bitacoraServicio;
        }

        public IActionResult Index()
        {
            var modelo = _servicio.ObtenerPagos();
            return View(modelo);
        }

        public IActionResult Registrar()
        {
            var suscripciones = _suscripcionServicio.ObtenerSuscripciones();
            ViewBag.Suscripciones = suscripciones;
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(Pago pago)
        {
            try
            {
                _servicio.RegistrarPago(pago);
                _bitacoraServicio.RegistrarAccion(User.Identity.Name, "Registró pago", $"Suscripción ID: {pago.SuscripcionId}, Monto: ${pago.Monto:N2}, Método: {pago.MetodoPago}");
                TempData["Exito"] = "El pago fue registrado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al registrar el pago: " + ex.Message;
                ViewBag.Suscripciones = _suscripcionServicio.ObtenerSuscripciones();
                return View(pago);
            }
        }
    }
}
