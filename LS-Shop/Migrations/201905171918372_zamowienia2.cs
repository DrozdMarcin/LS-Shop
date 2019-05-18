namespace LS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zamowienia2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "UserData_PostalCode", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "UserData_PostalCode", c => c.String(nullable: false));
        }
    }
}
