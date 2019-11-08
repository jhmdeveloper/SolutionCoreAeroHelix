using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SolutionCoreAeroHelix.Models
{
    public class Ruta
    {
        [Key]
        public int RutaID { get; set; }

        [Required(ErrorMessage = "Descripción Requerida.")]
        public string Descripcion { get; set; }

        public int LocacionOrigenID { get; set; }
        public int LocacionDestinoID { get; set; }

        public virtual Locacion LocacionOrigen { get; set; }
        public virtual Locacion LocacionDestino { get; set; }

        [Required(ErrorMessage = "Duración Requerida.")]
        public int Duracion { get; set; }


        [Required(ErrorMessage = "Estado Requerido.")]
        public int Estado { get; set; }

    }
}