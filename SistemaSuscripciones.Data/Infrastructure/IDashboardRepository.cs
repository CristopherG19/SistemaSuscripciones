using SistemaSuscripciones.Entities;
using System.Collections.Generic;

namespace SistemaSuscripciones.Data.Infrastructure
{
    public interface IDashboardRepository
    {
        ResumenDashboard ObtenerResumen();
        List<Suscripcion> ObtenerSuscripcionesPorVencer();
        List<DistribucionPlan> ObtenerDistribucionPorPlan();
    }
}
