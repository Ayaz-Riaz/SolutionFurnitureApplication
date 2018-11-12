namespace FurnitureApplication.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newEntityadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "orderedAt", c => c.DateTime(nullable: false));
            DropColumn("dbo.Orders", "ordereddAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "ordereddAt", c => c.DateTime(nullable: false));
            DropColumn("dbo.Orders", "orderedAt");
        }
    }
}
