namespace Vnet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class comercio : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VnetRegistroes", "ComercioId", c => c.Int(nullable: false));
            AddColumn("dbo.VnetRegistroes", "Comercio_IdComercio", c => c.Int());
            CreateIndex("dbo.VnetRegistroes", "Comercio_IdComercio");
            AddForeignKey("dbo.VnetRegistroes", "Comercio_IdComercio", "dbo.Comercios", "IdComercio");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VnetRegistroes", "Comercio_IdComercio", "dbo.Comercios");
            DropIndex("dbo.VnetRegistroes", new[] { "Comercio_IdComercio" });
            DropColumn("dbo.VnetRegistroes", "Comercio_IdComercio");
            DropColumn("dbo.VnetRegistroes", "ComercioId");
        }
    }
}
