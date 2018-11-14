namespace FurnitureApplication.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class discountprice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "OriginalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "HasDiscount", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "DiscountPercentage", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "DiscountPercentage");
            DropColumn("dbo.Products", "HasDiscount");
            DropColumn("dbo.Products", "OriginalPrice");
        }
    }
}
