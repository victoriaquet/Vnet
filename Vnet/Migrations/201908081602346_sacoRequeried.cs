namespace Vnet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sacoRequeried : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VnetRegistroes", "Descuento", c => c.Int());
            AlterColumn("dbo.VnetRegistroes", "Importe", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.VnetRegistroes", "ImporteDescuento", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.VnetRegistroes", "Descripcion", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VnetRegistroes", "Descripcion", c => c.String(nullable: false));
            AlterColumn("dbo.VnetRegistroes", "ImporteDescuento", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.VnetRegistroes", "Importe", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.VnetRegistroes", "Descuento", c => c.Int(nullable: false));
        }
    }
}
