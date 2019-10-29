using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SolutionCoreAeroHelix.Models
{
    public class Aeronave
    {
        [Key]
        public int AeronaveID { get; set; }


        [Required(ErrorMessage = "Nombre Requerido.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Modelo Requerido.")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "Modelo Requerido.")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "Año Requerido.")]
        public int Anio { get; set; }

        [Required(ErrorMessage = "Comentarios Requerido.")]
        public string Comentarios { get; set; }

        [Required(ErrorMessage = "Estado Requerido.")]
        public int Estado { get; set; }
    }
}