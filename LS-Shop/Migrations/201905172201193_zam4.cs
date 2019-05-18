namespace LS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zam4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "UserData_Email", c => c.String());
            AlterColumn("dbo.AspNetUsers", "UserData_FirstName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "UserData_LastName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "UserData_Street", c => c.String());
            AlterColumn("dbo.AspNetUsers", "UserData_City", c => c.String());
            AlterColumn("dbo.AspNetUsers", "UserData_PhoneNumber", c => c.String());
            AlterColumn("dbo.AspNetUsers", "UserData_PostalCode", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "UserData_PostalCode", c => c.String(nullable: false, maxLength: 6));
            AlterColumn("dbo.AspNetUsers", "UserData_PhoneNumber", c => c.String(maxLength: 12));
            AlterColumn("dbo.AspNetUsers", "UserData_City", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.AspNetUsers", "UserData_Street", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.AspNetUsers", "UserData_LastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.AspNetUsers", "UserData_FirstName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.AspNetUsers", "UserData_Email", c => c.String(nullable: false));
        }
    }
}
