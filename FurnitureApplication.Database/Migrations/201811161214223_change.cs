namespace FurnitureApplication.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "UserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "UserName", c => c.String());
        }
    }
}
