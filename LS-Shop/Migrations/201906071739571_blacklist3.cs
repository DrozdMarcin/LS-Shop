namespace LS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class blacklist3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Settings", "QuantityOfCanceledOrdersLimit", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Settings", "QuantityOfCanceledOrdersLimit", c => c.Int(nullable: false));
        }
    }
}
