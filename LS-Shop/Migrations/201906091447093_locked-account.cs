namespace LS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lockedaccount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsLocked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsLocked");
        }
    }
}
