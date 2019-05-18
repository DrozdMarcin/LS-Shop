namespace LS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zamowienia : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "UserData_PostalCode", c => c.String(nullable: true));
            CreateIndex("dbo.Order", "UserId");
            AddForeignKey("dbo.Order", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Order", new[] { "UserId" });
            AlterColumn("dbo.AspNetUsers", "UserData_PostalCode", c => c.String());
            DropColumn("dbo.Order", "UserId");
        }
    }
}
