using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SolutionCoreAeroHelix.Models
{
    public class Locacion
    {
        public Locacion()
        {
            Latitud = "-1";
            Longitud = "-1";
            Geolocalizacion = "-1";
        }


        [Key]
        public int LocacionID { get; set; }

        [Required(ErrorMessage = "Nombre Requerido.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Descripción Requerido.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Direccion Requerido.")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Latitud Requerido.")]
        public string Latitud { get; set; }

        [Required(ErrorMessage = "Longitud Requerido.")]
        public string Longitud { get; set; }

        [Required(ErrorMessage = "Horario de Servicio Requerido.")]
        public string HorarioServicio { get; set; }

        public string Geolocalizacion { get; set; }

        public virtual ICollection<Ruta> RutasOrigen { get; set; }
        public virtual ICollection<Ruta> RutasDestino { get; set; }

        public virtual ICollection<Reservacion> ReservacionesOrigen { get; set; }
        public virtual ICollection<Reservacion> ReservacionesDestino { get; set; }


    }
}