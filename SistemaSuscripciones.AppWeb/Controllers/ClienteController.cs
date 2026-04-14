using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SistemaSuscripciones.BusinessLogic;
using SistemaSuscripciones.Entities;

namespace SistemaSuscripciones.AppWeb.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        private readonly IClienteService _servicio;
        private readonly IBitacoraService _bitacoraServicio;

        public ClienteController(IClienteService servicio, IBitacoraService bitacoraServicio)
        {
            _servicio = servicio;
            _bitacoraServicio = bitacoraServicio;
        }

        public IActionResult Index()
        {
            var modelo = _servicio.ObtenerClientes();
            return View(modelo);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Cliente cliente)
        {
            try
            {
                _servicio.RegistrarCliente(cliente);
                _bitacoraServicio.RegistrarAccion(User.Identity.Name, "Registró cliente", $"{cliente.Nombres} {cliente.Apellidos} - Doc: {cliente.DocumentoIdentidad}");
                TempData["Exito"] = "El cliente fue registrado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al registrar el cliente: " + ex.Message;
                return View(cliente);
            }
        }

        public IActionResult Editar(int id)
        {
            var cliente = _servicio.ObtenerCliente(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        public IActionResult Editar(Cliente cliente)
        {
            try
            {
                _servicio.EditarCliente(cliente);
                _bitacoraServicio.RegistrarAccion(User.Identity.Name, "Editó cliente", $"Cliente ID: {cliente.Id} - {cliente.Nombres} {cliente.Apellidos}");
                TempData["Exito"] = "Los datos del cliente fueron actualizados.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al actualizar el cliente: " + ex.Message;
                return View(cliente);
            }
        }

        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            try
            {
                _servicio.EliminarCliente(id);
                _bitacoraServicio.RegistrarAccion(User.Identity.Name, "Desactivó cliente", $"Cliente ID: {id}");
                TempData["Exito"] = "El cliente fue desactivado del sistema.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al eliminar el cliente: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
