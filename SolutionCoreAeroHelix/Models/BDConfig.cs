using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SolutionCoreAeroHelix.Models
{
    public class BDConfig : DbContext
    {
        public BDConfig()
        {
            Database.SetInitializer<BDConfig>(null); //Evita el error de modificacion de tablas desde la base de datos
        }

        public System.Data.Entity.DbSet<SolutionCoreAeroHelix.Models.Cliente> Clientes { get; set; }

        public System.Data.Entity.DbSet<SolutionCoreAeroHelix.Models.Usuario> Usuarios { get; set; }

        public System.Data.Entity.DbSet<SolutionCoreAeroHelix.Models.Aeronave> Aeronaves { get; set; }

        public System.Data.Entity.DbSet<SolutionCoreAeroHelix.Models.Capitan> Capitans { get; set; }

        public System.Data.Entity.DbSet<SolutionCoreAeroHelix.Models.Locacion> Locacions { get; set; }

        public System.Data.Entity.DbSet<SolutionCoreAeroHelix.Models.Reservacion> Reservaciones { get; set; }
    }
}