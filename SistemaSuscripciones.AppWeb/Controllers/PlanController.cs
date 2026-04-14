using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SistemaSuscripciones.BusinessLogic;
using SistemaSuscripciones.Entities;

namespace SistemaSuscripciones.AppWeb.Controllers
{
    [Authorize]
    public class PlanController : Controller
    {
        private readonly IPlanService _servicio;

        public PlanController(IPlanService servicio)
        {
            _servicio = servicio;
        }

        // GET: Listado de planes
        public IActionResult Index()
        {
            var modelo = _servicio.ObtenerPlanes();
            return View(modelo);
        }

        // GET: Formulario de creación
        public IActionResult Crear()
        {
            return View();
        }

        // POST: Registrar nuevo plan
        [HttpPost]
        public IActionResult Crear(Plan plan)
        {
            try
            {
                _servicio.RegistrarPlan(plan);
                TempData["Exito"] = "El plan fue registrado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al registrar el plan: " + ex.Message;
                return View(plan);
            }
        }

        // GET: Formulario de edición
        public IActionResult Editar(int id)
        {
            var plan = _servicio.ObtenerPlan(id);
            if (plan == null)
            {
                return NotFound();
            }
            return View(plan);
        }

        // POST: Actualizar plan
        [HttpPost]
        public IActionResult Editar(Plan plan)
        {
            try
            {
                _servicio.EditarPlan(plan);
                TempData["Exito"] = "El plan fue actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al actualizar el plan: " + ex.Message;
                return View(plan);
            }
        }

        // POST: Eliminar plan
        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            try
            {
                _servicio.EliminarPlan(id);
                TempData["Exito"] = "El plan fue eliminado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "No se pudo eliminar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
