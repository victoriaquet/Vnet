namespace Vnet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class solucionadolong : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VnetRegistroes", "NroTarjeta", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VnetRegistroes", "NroTarjeta", c => c.Int(nullable: false));
        }
    }
}
