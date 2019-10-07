using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SolutionCoreAeroHelix.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioID { get; set; }

        [Required(ErrorMessage = "Nombre usuario Requerido.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Contraseña Requerida.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Usuario y/o Contraseña inválidos.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Estado Requerido.")]
        public int Estado { get; set; }

        public int ClienteID { get; set; }
        public virtual Cliente cliente { get; set; }
    }
}