namespace Vnet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingVnetRegistro : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VnetRegistroes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NroRegistro = c.Int(nullable: false),
                        NroEstablecimiento = c.Int(nullable: false),
                        NroTerminal = c.Int(nullable: false),
                        FechaHoraMov = c.DateTime(nullable: false),
                        NroTarjeta = c.Int(nullable: false),
                        Descuento = c.Int(nullable: false),
                        Importe = c.Single(nullable: false),
                        ImporteDescuento = c.Single(nullable: false),
                        Descripcion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VnetRegistroes");
        }
    }
}
