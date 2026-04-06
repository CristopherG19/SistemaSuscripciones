using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaSuscripciones.Entities
{
    public class Plan
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int DuracionMeses { get; set; }
    }
}
