namespace RollingStockDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveDeleted : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RollingStocks", "_Is_Deleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RollingStocks", "_Is_Deleted", c => c.Boolean());
        }
    }
}
