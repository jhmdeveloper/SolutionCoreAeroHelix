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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ruta>()
                        .HasRequired(m => m.LocacionOrigen)
                        .WithMany(t => t.RutasOrigen)
                        .HasForeignKey(m => m.LocacionOrigenID)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ruta>()
                        .HasRequired(m => m.LocacionDestino)
                        .WithMany(t => t.RutasDestino)
                        .HasForeignKey(m => m.LocacionDestinoID)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reservacion>()
                       .HasRequired(m => m.LocacionOrigen)
                       .WithMany(t => t.ReservacionesOrigen)
                       .HasForeignKey(m => m.LocacionOrigenID)
                       .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reservacion>()
                        .HasRequired(m => m.LocacionDestino)
                        .WithMany(t => t.ReservacionesDestino)
                        .HasForeignKey(m => m.LocacionDestinoID)
                        .WillCascadeOnDelete(false);
        }

        public System.Data.Entity.DbSet<SolutionCoreAeroHelix.Models.Cliente> Clientes { get; set; }

        public System.Data.Entity.DbSet<SolutionCoreAeroHelix.Models.Usuario> Usuarios { get; set; }

        public System.Data.Entity.DbSet<SolutionCoreAeroHelix.Models.Aeronave> Aeronaves { get; set; }

        public System.Data.Entity.DbSet<SolutionCoreAeroHelix.Models.Capitan> Capitans { get; set; }

        public System.Data.Entity.DbSet<SolutionCoreAeroHelix.Models.Locacion> Locacions { get; set; }

        public System.Data.Entity.DbSet<SolutionCoreAeroHelix.Models.Ruta> Rutas { get; set; }

        public System.Data.Entity.DbSet<SolutionCoreAeroHelix.Models.Reservacion> Reservacions { get; set; }

        public System.Data.Entity.DbSet<SolutionCoreAeroHelix.Models.Bolsa> Bolsas { get; set; }

        public System.Data.Entity.DbSet<SolutionCoreAeroHelix.Models.ComentarioReservacion> ComentarioReservacions { get; set; }

        public System.Data.Entity.DbSet<SolutionCoreAeroHelix.Models.Status> Status { get; set; }

        public System.Data.Entity.DbSet<SolutionCoreAeroHelix.Models.Perfil> Perfils { get; set; }
    }
}