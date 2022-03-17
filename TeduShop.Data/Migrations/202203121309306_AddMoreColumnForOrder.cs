namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMoreColumnForOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "AddressDistrict", c => c.String());
            AddColumn("dbo.Orders", "AddressCity", c => c.String());
            AddColumn("dbo.Orders", "AddressWard", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "AddressWard");
            DropColumn("dbo.Orders", "AddressCity");
            DropColumn("dbo.Orders", "AddressDistrict");
        }
    }
}
