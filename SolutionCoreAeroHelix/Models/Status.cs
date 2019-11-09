using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SolutionCoreAeroHelix.Models
{
    public class Status
    {
        [Key]
        public int StatusID { get; set; }

        [Required(ErrorMessage = "Descripción requerida.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Clase de color requerido.")]
        public string Class { get; set; }
    }
}