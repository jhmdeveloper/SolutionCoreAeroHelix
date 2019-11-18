using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolutionCoreAeroHelix.Models
{
    public class Reservacion
    {
        [Key, Display(Name = "Folio")]
        public int ReservacionID { get; set; }

        [Required(ErrorMessage = "El usuario es requerido.")]
        public int UsuarioID { get; set; }

        [Display(Name = "Usuario solicitante")]
        public virtual Usuario Usuario { get; set; }

        [Display(Name = "Origen"), Required(ErrorMessage = "El origen es requerido.")]
        public int LocacionOrigenID { get; set; }

        public virtual Locacion LocacionOrigen { get; set; }

        [Display(Name = "Origen detalle dirección"), 
            Required(ErrorMessage = "La dirección de origen es requerida.")]
        public string DireccionOrigen { get; set; }

        [Display(Name = "Destino"), Required(ErrorMessage = "El destino es requerido.")]
        public int LocacionDestinoID { get; set; }

        public virtual Locacion LocacionDestino { get; set; }

        [Display(Name = "Destino detalle dirección"), 
            Required(ErrorMessage = "La dirección de destino es requerida.")]
        public string DireccionDestino { get; set; }

        [Display(Name = "Fecha y hora", Prompt = "dd/mm/aaaa hh:mm"),
            DataType(DataType.DateTime, ErrorMessage = "El formato de fecha debe ser aaaa/mm/dd hh:mm"),
            Required(ErrorMessage = "La fecha y hora son requeridas.")]
        public DateTime FechaHora { get; set; }

        [Display(Name = "Duración estimada del vuelo (mins)"), 
            Required(ErrorMessage = "La duración de vuelo es requerida."),
            RegularExpression(@"^\d+", ErrorMessage = "Capturar sólo dígitos.")]
        public int DuracionVuelo { get; set; }

        [Display(Name = "Total de pasajeros"),
            Range(1, 3, ErrorMessage = "Indicar mínimo un pasajero y máximo tres."),
            Required(ErrorMessage = "Total de pasajeros requerido."),
            RegularExpression(@"^\d+", ErrorMessage = "Capturar sólo dígitos.")]
        public int TotalPasajeros { get; set; }

        [Display(Name = "Requiere equipaje.")]
        public bool Equipaje { get; set; }

        [Display(Name = "Comentarios"), NotMapped, 
            Required(ErrorMessage = "Los comentarios son requeridos.")]
        public string Comentario { get; set; }

        [Display(Name = "Status"),
            Required(ErrorMessage = "El status es requerido.")]
        public int StatusID { get; set; }

        [Display(Name = "Status")]
        public virtual Status Status { get; set; }
    }
}