namespace LS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class integrateidentity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "UserData_Name", c => c.String());
            AlterColumn("dbo.AspNetUsers", "UserData_LastName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "UserData_Adress", c => c.String());
            AlterColumn("dbo.AspNetUsers", "UserData_City", c => c.String());
            AlterColumn("dbo.AspNetUsers", "UserData_Phone", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "UserData_Phone", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "UserData_City", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "UserData_Adress", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "UserData_LastName", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "UserData_Name", c => c.String(nullable: false));
        }
    }
}
