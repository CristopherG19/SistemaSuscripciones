using SistemaSuscripciones.Entities;
using System.Collections.Generic;

namespace SistemaSuscripciones.BusinessLogic
{
    public interface IDashboardService
    {
        ResumenDashboard ObtenerResumen();
        List<Suscripcion> ObtenerProximasAVencer();
        List<DistribucionPlan> ObtenerDistribucion();
    }
}
