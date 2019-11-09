using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SolutionCoreAeroHelix.Models
{
    public class Perfil
    {
        [Key]
        public int PerfilID { get; set; }

        [Required(ErrorMessage = "Descripción requerida.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Página de inicio requerida.")]
        public string PaginaInicio { get; set; }
    }
}