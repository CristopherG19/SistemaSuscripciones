using Microsoft.AspNetCore.Mvc;
using SistemaSuscripciones.BusinessLogic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SistemaSuscripciones.AppWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public LoginController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // Muestra la pantalla de Login
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Ingresar(string correo, string clave)
        {
            var usuario = _usuarioService.Autenticar(correo, clave);

            if (usuario != null)
            {
                // 1. Creamos los "Claims" (Datos del usuario que guardará la cookie)
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.NombreCompleto),
                    new Claim(ClaimTypes.Email, usuario.Correo),
                    new Claim(ClaimTypes.Role, usuario.Rol)
                };

                // 2. Creamos la identidad
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // 3. Iniciamos sesión en el sistema
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Suscripcion");
            }
            else
            {
                ViewBag.Error = "Correo o contraseña incorrectos";
                return View("Index");
            }
        }

        public async Task<IActionResult> Salir()
        {
            // Borra la cookie y cierra la sesión
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}