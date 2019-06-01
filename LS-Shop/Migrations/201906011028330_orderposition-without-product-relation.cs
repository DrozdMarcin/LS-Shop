namespace LS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderpositionwithoutproductrelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderPosition", "ProductId", "dbo.Product");
            DropIndex("dbo.OrderPosition", new[] { "ProductId" });
            RenameColumn(table: "dbo.OrderPosition", name: "ProductId", newName: "Product_ProductId");
            AddColumn("dbo.OrderPosition", "ProductName", c => c.String());
            AlterColumn("dbo.OrderPosition", "Product_ProductId", c => c.Int());
            CreateIndex("dbo.OrderPosition", "Product_ProductId");
            AddForeignKey("dbo.OrderPosition", "Product_ProductId", "dbo.Product", "ProductId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderPosition", "Product_ProductId", "dbo.Product");
            DropIndex("dbo.OrderPosition", new[] { "Product_ProductId" });
            AlterColumn("dbo.OrderPosition", "Product_ProductId", c => c.Int(nullable: false));
            DropColumn("dbo.OrderPosition", "ProductName");
            RenameColumn(table: "dbo.OrderPosition", name: "Product_ProductId", newName: "ProductId");
            CreateIndex("dbo.OrderPosition", "ProductId");
            AddForeignKey("dbo.OrderPosition", "ProductId", "dbo.Product", "ProductId", cascadeDelete: true);
        }
    }
}
