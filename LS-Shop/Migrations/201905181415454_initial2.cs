namespace LS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "Order_OrderId", c => c.Int());
            CreateIndex("dbo.Product", "Order_OrderId");
            AddForeignKey("dbo.Product", "Order_OrderId", "dbo.Order", "OrderId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "Order_OrderId", "dbo.Order");
            DropIndex("dbo.Product", new[] { "Order_OrderId" });
            DropColumn("dbo.Product", "Order_OrderId");
        }
    }
}
