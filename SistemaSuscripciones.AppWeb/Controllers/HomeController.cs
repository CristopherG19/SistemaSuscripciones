using SistemaSuscripciones.AppWeb.Models;
using SistemaSuscripciones.BusinessLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace SistemaSuscripciones.AppWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IDashboardService _dashboardServicio;

        public HomeController(IDashboardService dashboardServicio)
        {
            _dashboardServicio = dashboardServicio;
        }

        public IActionResult Index()
        {
            var viewModel = new DashboardViewModel
            {
                Resumen = _dashboardServicio.ObtenerResumen(),
                ProximasAVencer = _dashboardServicio.ObtenerProximasAVencer(),
                DistribucionPorPlan = _dashboardServicio.ObtenerDistribucion()
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
