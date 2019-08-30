namespace Vnet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class comercioRequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VnetRegistroes", "Comercio_IdComercio", "dbo.Comercios");
            DropIndex("dbo.VnetRegistroes", new[] { "Comercio_IdComercio" });
            AlterColumn("dbo.VnetRegistroes", "Comercio_IdComercio", c => c.Int(nullable: false));
            CreateIndex("dbo.VnetRegistroes", "Comercio_IdComercio");
            AddForeignKey("dbo.VnetRegistroes", "Comercio_IdComercio", "dbo.Comercios", "IdComercio", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VnetRegistroes", "Comercio_IdComercio", "dbo.Comercios");
            DropIndex("dbo.VnetRegistroes", new[] { "Comercio_IdComercio" });
            AlterColumn("dbo.VnetRegistroes", "Comercio_IdComercio", c => c.Int());
            CreateIndex("dbo.VnetRegistroes", "Comercio_IdComercio");
            AddForeignKey("dbo.VnetRegistroes", "Comercio_IdComercio", "dbo.Comercios", "IdComercio");
        }
    }
}
