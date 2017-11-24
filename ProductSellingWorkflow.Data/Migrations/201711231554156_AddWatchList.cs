namespace ProductSellingWorkflow.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWatchList : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WatchLists",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ProductId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WatchLists", "ProductId", "dbo.Products");
            DropForeignKey("dbo.WatchLists", "UserId", "dbo.Users");
            DropIndex("dbo.WatchLists", new[] { "ProductId" });
            DropIndex("dbo.WatchLists", new[] { "UserId" });
            DropTable("dbo.WatchLists");
        }
    }
}
