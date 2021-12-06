namespace RollingStockDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RollingStocks", "Deleted", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RollingStocks", "Deleted");
        }
    }
}
