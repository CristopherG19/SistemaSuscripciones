using System;

namespace SistemaSuscripciones.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string DocumentoIdentidad { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }

        // Propiedad calculada para mostrar el nombre completo en las vistas
        public string NombreCompleto => $"{Nombres} {Apellidos}";
    }
}
