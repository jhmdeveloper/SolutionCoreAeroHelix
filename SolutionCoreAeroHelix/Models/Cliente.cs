using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SolutionCoreAeroHelix.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteID { get; set; }

        [Required(ErrorMessage = "Nombre de cliente requerido.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "RFC requerido")]
        public string RFC { get; set; }

        [Required(ErrorMessage = "Correo Requerido.")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "Telefono contacto requerido Requerido.")]
        public string TelefonoContacto { get; set; }

        [Required(ErrorMessage = "Dirección requerida")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Latitud Requerida")]
        public string GeoLatitud { get; set; }

        [Required(ErrorMessage = "Longitud Requerida")]
        public string GeoLongitud { get; set; }

        [Required(ErrorMessage = "Estado Requerido.")]
        public int Estado { get; set; }

        public virtual ICollection<Usuario> usuarios { get; set; }
    }
}