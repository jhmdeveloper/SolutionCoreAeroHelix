using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SolutionCoreAeroHelix.Models
{
    public class Bolsa
    {
        [Key]
        public int BolsaID { get; set; }

        public int ClienteID { get; set; }
        public virtual Cliente cliente { get; set; }

        public int Minutos { get; set; }

        public int MinutosRestantes { get; set; }

    }
}