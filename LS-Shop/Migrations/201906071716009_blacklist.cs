namespace LS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class blacklist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserData_QuantityOfCanceledOrders", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "UserData_IsOnBlackList", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UserData_IsOnBlackList");
            DropColumn("dbo.AspNetUsers", "UserData_QuantityOfCanceledOrders");
        }
    }
}
