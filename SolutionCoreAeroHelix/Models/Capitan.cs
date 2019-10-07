using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SolutionCoreAeroHelix.Models
{
    public class Capitan
    {
        [Key]
        public int CapitanID { get; set; }

        [Required(ErrorMessage = "Nombre Requerido.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Comentarios Requerido.")]
        public string Comentarios { get; set; }

        public string Licencia { get; set; }
    }
}