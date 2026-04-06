using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaSuscripciones.Entities
{
    public class Suscripcion
    {
        public int Id { get; set; }
        public string ClienteNombre { get; set; }
        public string DocumentoIdentidad { get; set; }
        public int PlanId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Estado { get; set; }
        public string NombrePlan { get; set; } // Propiedad auxiliar para la vista
    }
}