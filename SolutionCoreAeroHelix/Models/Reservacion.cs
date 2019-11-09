using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolutionCoreAeroHelix.Models
{
    public class Reservacion
    {
        [Key, Display(Name = "Folio")]
        public int ReservacionID { get; set; }

        [Required(ErrorMessage = "Usuario requerido.")]
        public int UsuarioID { get; set; }

        [Display(Name = "Usuario solicitante")]
        public virtual Usuario usuario { get; set; }

        [Display(Name = "Origen"), Required(ErrorMessage = "Origen requerido.")]
        public int LocacionOrigenID { get; set; }

        [Display(Name = "Destino"), Required(ErrorMessage = "Destino requerido.")]
        public int LocacionDestinoID { get; set; }

        public virtual Locacion LocacionOrigen { get; set; }

        public virtual Locacion LocacionDestino { get; set; }

        [Display(Name = "Fecha y hora"), Required(ErrorMessage = "Fecha y hora requerida.")]
        public DateTime FechaHora { get; set; }

        [Display(Name = "Total de pasajeros"), Required(ErrorMessage = "Total de pasajeros requerido.")]
        public int TotalPasajeros { get; set; }

        [Display(Name = "Origen detalle dirección"), Required(ErrorMessage = "Dirección origen requerida.")]
        public string DireccionOrigen { get; set; }

        [Display(Name = "Destino detalle dirección"), Required(ErrorMessage = "Dirección destino requerida.")]
        public string DireccionDestino { get; set; }

        [Display(Name = "Duración estimada del vuelo (mins)"), Required(ErrorMessage = "Duración vuelo requerido.")]
        public int DuracionVuelo { get; set; }

        [Display(Name = "Requiere equipaje")]
        public bool Equipaje { get; set; }

        [Display(Name = "Comentarios"), NotMapped, Required(ErrorMessage = "Comentario requerido.")]
        public string Comentario { get; set; }

        [Display(Name = "Status")]
        public int StatusID { get; set; }

        [Display(Name = "Status")]
        public virtual Status Status { get; set; }
    }
}