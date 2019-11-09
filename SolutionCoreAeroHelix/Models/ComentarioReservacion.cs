using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolutionCoreAeroHelix.Models
{
    public class ComentarioReservacion
    {
        public ComentarioReservacion()
        {
            Fecha = DateTime.Now;
            StatusID = 1;
        }

        [Key]
        public long ComentarioID { get; set; }

        [Required(ErrorMessage = "Falta ID de reservación")]
        public int ReservacionID { get; set; }

        [Required(ErrorMessage = "Usuario requerido.")]
        public int UsuarioID { get; set; }

        [Required(ErrorMessage = "Comentario requerido.")]
        public string Comentario { get; set; }
        
        public DateTime Fecha { get; set; }
        
        public int StatusID { get; set; }

        public virtual Status Status { get; set; }
    }
}