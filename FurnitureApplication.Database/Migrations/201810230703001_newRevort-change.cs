namespace FurnitureApplication.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newRevortchange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "CategoryID" });
            RenameColumn(table: "dbo.Products", name: "CategoryID", newName: "Category_ID");
            AlterColumn("dbo.Categories", "Name", c => c.String());
            AlterColumn("dbo.Categories", "Description", c => c.String());
            AlterColumn("dbo.Products", "Category_ID", c => c.Int());
            AlterColumn("dbo.Products", "Name", c => c.String());
            AlterColumn("dbo.Products", "Description", c => c.String());
            CreateIndex("dbo.Products", "Category_ID");
            AddForeignKey("dbo.Products", "Category_ID", "dbo.Categories", "ID");
            DropColumn("dbo.Categories", "ImageUrl");
            DropColumn("dbo.Categories", "isFeatured");
            DropColumn("dbo.Products", "ImageUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ImageUrl", c => c.String());
            AddColumn("dbo.Categories", "isFeatured", c => c.Boolean(nullable: false));
            AddColumn("dbo.Categories", "ImageUrl", c => c.String());
            DropForeignKey("dbo.Products", "Category_ID", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "Category_ID" });
            AlterColumn("dbo.Products", "Description", c => c.String(maxLength: 500));
            AlterColumn("dbo.Products", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.Products", "Category_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Categories", "Description", c => c.String(maxLength: 500));
            AlterColumn("dbo.Categories", "Name", c => c.String(maxLength: 50));
            RenameColumn(table: "dbo.Products", name: "Category_ID", newName: "CategoryID");
            CreateIndex("dbo.Products", "CategoryID");
            AddForeignKey("dbo.Products", "CategoryID", "dbo.Categories", "ID", cascadeDelete: true);
        }
    }
}
