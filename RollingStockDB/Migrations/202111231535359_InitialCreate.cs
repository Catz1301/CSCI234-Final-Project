namespace RollingStockDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
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
            
            CreateTable(
                "dbo.RollingStocks",
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
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.RollingStocks", new[] { "Id" });
            DropTable("dbo.RollingStocks");
            DropTable("dbo.DeletedRollingStocks");
        }
    }
}
