using SistemaSuscripciones.Entities;
using SistemaSuscripciones.Data.Infrastructure;
using System.Collections.Generic;

namespace SistemaSuscripciones.BusinessLogic
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _repositorio;

        public DashboardService(IDashboardRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public ResumenDashboard ObtenerResumen()
        {
            return _repositorio.ObtenerResumen();
        }

        public List<Suscripcion> ObtenerProximasAVencer()
        {
            return _repositorio.ObtenerSuscripcionesPorVencer();
        }

        public List<DistribucionPlan> ObtenerDistribucion()
        {
            return _repositorio.ObtenerDistribucionPorPlan();
        }
    }
}
