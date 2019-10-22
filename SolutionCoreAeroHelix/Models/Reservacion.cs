using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SolutionCoreAeroHelix.Models
{
    public class Reservacion
    {
        public Reservacion()
        {
            //Valores predeterminados
            Status = 1;
            Registro = DateTime.Now;
        }

        [Key]
        public int ReservacionID { get; set; }

        [Required(ErrorMessage = "La fecha es requerida.")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El horario es requerido.")]
        [DataType(DataType.Time)]
        public DateTime Hora { get; set; }

        [Required(ErrorMessage = "El número de personas es requerido.")]
        public int Personas { get; set; }
        
        public int Status { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime Registro { get; set; }

        public int ClienteID { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}