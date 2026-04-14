using System;

namespace SistemaSuscripciones.Entities
{
    public class Bitacora
    {
        public int Id { get; set; }
        public int? UsuarioId { get; set; }
        public string NombreUsuario { get; set; }
        public string Accion { get; set; }
        public string Detalle { get; set; }
        public DateTime FechaHora { get; set; }
    }
}
