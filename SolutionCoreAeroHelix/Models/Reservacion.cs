using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SolutionCoreAeroHelix.Models
{
    public class Reservacion
    {
        [Key]
        public int ReservacionID { get; set; }

        public int UsuarioID { get; set; }
        public virtual Usuario usuario { get; set; }

        public int LocacionOrigenID { get; set; }
        public int LocacionDestinoID { get; set; }

        public virtual Locacion LocacionOrigen { get; set; }
        public virtual Locacion LocacionDestino { get; set; }

        public DateTime FechaHora { get; set; }

        public int TotalPasajeros { get; set; }

        public string DireccionOrigen { get; set; }
        public string DireccionDestino { get; set; }

        public int DuracionVuelo { get; set; }

        public bool Equipaje { get; set; }

        [Required(ErrorMessage = "Comentarios Requerido.")]
        public string Comentarios { get; set; }

        public int Status { get; set; }
    }
}