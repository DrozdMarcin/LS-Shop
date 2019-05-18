namespace LS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zamowienia3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserData_Email", c => c.String(nullable: true));
            AddColumn("dbo.AspNetUsers", "UserData_FirstName", c => c.String(nullable: true, maxLength: 50));
            AddColumn("dbo.AspNetUsers", "UserData_Street", c => c.String(nullable: true, maxLength: 100));
            AddColumn("dbo.AspNetUsers", "UserData_PhoneNumber", c => c.String(maxLength: 12));
            AlterColumn("dbo.AspNetUsers", "UserData_LastName", c => c.String(nullable: true, maxLength: 50));
            AlterColumn("dbo.AspNetUsers", "UserData_City", c => c.String(nullable: true, maxLength: 100));
            AlterColumn("dbo.AspNetUsers", "UserData_PostalCode", c => c.String(nullable: true, maxLength: 6));
            DropColumn("dbo.AspNetUsers", "UserData_Name");
            DropColumn("dbo.AspNetUsers", "UserData_Adress");
            DropColumn("dbo.AspNetUsers", "UserData_Phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserData_Phone", c => c.String());
            AddColumn("dbo.AspNetUsers", "UserData_Adress", c => c.String());
            AddColumn("dbo.AspNetUsers", "UserData_Name", c => c.String());
            AlterColumn("dbo.AspNetUsers", "UserData_PostalCode", c => c.String());
            AlterColumn("dbo.AspNetUsers", "UserData_City", c => c.String());
            AlterColumn("dbo.AspNetUsers", "UserData_LastName", c => c.String());
            DropColumn("dbo.AspNetUsers", "UserData_PhoneNumber");
            DropColumn("dbo.AspNetUsers", "UserData_Street");
            DropColumn("dbo.AspNetUsers", "UserData_FirstName");
            DropColumn("dbo.AspNetUsers", "UserData_Email");
        }
    }
}
