namespace SistemaSuscripciones.Entities
{
    public class ResumenDashboard
    {
        public int TotalClientes { get; set; }
        public int SuscripcionesActivas { get; set; }
        public int SuscripcionesCanceladas { get; set; }
        public decimal IngresosMensuales { get; set; }
    }
}
