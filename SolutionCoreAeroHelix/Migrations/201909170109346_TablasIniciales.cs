namespace SolutionCoreAeroHelix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TablasIniciales : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Aeronaves",
                c => new
                    {
                        AeronaveID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Modelo = c.String(nullable: false),
                        Tipo = c.String(nullable: false),
                        Anio = c.Int(nullable: false),
                        Comentarios = c.String(nullable: false),
                        Estado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AeronaveID);
            
            CreateTable(
                "dbo.Capitans",
                c => new
                    {
                        CapitanID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Comentarios = c.String(nullable: false),
                        Licencia = c.String(),
                    })
                .PrimaryKey(t => t.CapitanID);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        ClienteID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        RFC = c.String(nullable: false),
                        Correo = c.String(nullable: false),
                        TelefonoContacto = c.String(nullable: false),
                        Direccion = c.String(nullable: false),
                        GeoLatitud = c.String(nullable: false),
                        GeoLongitud = c.String(nullable: false),
                        Estado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClienteID);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        UsuarioID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        ConfirmPassword = c.String(),
                        Estado = c.Int(nullable: false),
                        ClienteID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UsuarioID)
                .ForeignKey("dbo.Clientes", t => t.ClienteID, cascadeDelete: true)
                .Index(t => t.ClienteID);
            
            CreateTable(
                "dbo.Locacions",
                c => new
                    {
                        LocacionID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Descripcion = c.String(nullable: false),
                        Direccion = c.String(nullable: false),
                        Latitud = c.String(nullable: false),
                        Longitud = c.String(nullable: false),
                        HorarioServicio = c.String(nullable: false),
                        Geolocalizacion = c.String(),
                    })
                .PrimaryKey(t => t.LocacionID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuarios", "ClienteID", "dbo.Clientes");
            DropIndex("dbo.Usuarios", new[] { "ClienteID" });
            DropTable("dbo.Locacions");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Clientes");
            DropTable("dbo.Capitans");
            DropTable("dbo.Aeronaves");
        }
    }
}
