namespace Vnet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correccionFloatADecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VnetRegistroes", "Importe", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.VnetRegistroes", "ImporteDescuento", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VnetRegistroes", "ImporteDescuento", c => c.Single(nullable: false));
            AlterColumn("dbo.VnetRegistroes", "Importe", c => c.Single(nullable: false));
        }
    }
}
