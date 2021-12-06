namespace RollingStockDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsDeleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RollingStocks", "_is_deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RollingStocks", "_is_deleted");
        }
    }
}
