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

        [Required(ErrorMessage = "Fecha y hora requerida.")]
        public DateTime FechaHora { get; set; }

        [Required(ErrorMessage = "Total de pasajeros requerido.")]
        public int TotalPasajeros { get; set; }

        [Required(ErrorMessage = "Dirección origen requerida.")]
        public string DireccionOrigen { get; set; }

        [Required(ErrorMessage = "Dirección destino requerida.")]
        public string DireccionDestino { get; set; }

        [Required(ErrorMessage = "Duración vuelo requerido.")]
        public int DuracionVuelo { get; set; }

        public bool Equipaje { get; set; }

        [Required(ErrorMessage = "Comentarios Requeridos.")]
        public string Comentarios { get; set; }

        public int Status { get; set; }
    }
}