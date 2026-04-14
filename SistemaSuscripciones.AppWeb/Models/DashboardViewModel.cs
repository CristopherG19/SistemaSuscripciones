using SistemaSuscripciones.Entities;
using System.Collections.Generic;

namespace SistemaSuscripciones.AppWeb.Models
{
    public class DashboardViewModel
    {
        public ResumenDashboard Resumen { get; set; }
        public List<Suscripcion> ProximasAVencer { get; set; }
        public List<DistribucionPlan> DistribucionPorPlan { get; set; }
    }
}
