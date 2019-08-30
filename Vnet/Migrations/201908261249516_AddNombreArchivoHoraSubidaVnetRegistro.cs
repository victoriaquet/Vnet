namespace Vnet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNombreArchivoHoraSubidaVnetRegistro : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VnetRegistroes", "NombreArchivo", c => c.String(nullable: false));
            AddColumn("dbo.VnetRegistroes", "HoraDeSubida", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VnetRegistroes", "HoraDeSubida");
            DropColumn("dbo.VnetRegistroes", "NombreArchivo");
        }
    }
}
