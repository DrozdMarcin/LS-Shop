namespace LS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class categoryhidden : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Category", "Hidden", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Category", "Hidden");
        }
    }
}
