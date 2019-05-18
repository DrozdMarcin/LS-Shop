namespace LS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class poprawki : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserData_PostalCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UserData_PostalCode");
        }
    }
}
