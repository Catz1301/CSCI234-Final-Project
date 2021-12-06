namespace RollingStockDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveDeletedRollingStocksTable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.DeletedRollingStocks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DeletedRollingStocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Is_Engine = c.Boolean(nullable: false),
                        Has_Load = c.Boolean(nullable: false),
                        Owning_Company = c.String(),
                        Stock_Type = c.Int(nullable: false),
                        Reporting_Marks = c.String(),
                        Fleet_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
