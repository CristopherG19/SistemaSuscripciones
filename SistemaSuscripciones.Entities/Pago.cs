using System;

namespace SistemaSuscripciones.Entities
{
    public class Pago
    {
        public int Id { get; set; }
        public int SuscripcionId { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
        public string MetodoPago { get; set; }
        public string Referencia { get; set; }
        public string Observaciones { get; set; }

        // Propiedades auxiliares para mostrar en las vistas (vienen del JOIN en los SPs)
        public string ClienteNombre { get; set; }
        public string NombrePlan { get; set; }
    }
}
