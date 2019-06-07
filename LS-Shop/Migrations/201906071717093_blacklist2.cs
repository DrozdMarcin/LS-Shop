namespace LS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class blacklist2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Settings", "QuantityOfCanceledOrdersLimit", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Settings", "QuantityOfCanceledOrdersLimit");
        }
    }
}
