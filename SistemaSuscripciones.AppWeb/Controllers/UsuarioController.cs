using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SistemaSuscripciones.BusinessLogic;
using SistemaSuscripciones.Entities;

namespace SistemaSuscripciones.AppWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _servicio;

        public UsuarioController(IUsuarioService servicio)
        {
            _servicio = servicio;
        }

        // GET: Listado de usuarios
        public IActionResult Index()
        {
            var modelo = _servicio.ObtenerUsuarios();
            return View(modelo);
        }

        // GET: Formulario de edición
        public IActionResult Editar(int id)
        {
            var usuario = _servicio.ObtenerUsuario(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Actualizar datos del usuario
        [HttpPost]
        public IActionResult Editar(Usuario usuario)
        {
            try
            {
                _servicio.EditarUsuario(usuario);
                TempData["Exito"] = "El usuario fue actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al actualizar el usuario: " + ex.Message;
                return View(usuario);
            }
        }

        // POST: Activar o desactivar usuario
        [HttpPost]
        public IActionResult CambiarEstado(int id, bool activo)
        {
            try
            {
                _servicio.CambiarEstadoUsuario(id, activo);
                string accion = activo ? "activado" : "desactivado";
                TempData["Exito"] = $"El usuario fue {accion} correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cambiar el estado: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
