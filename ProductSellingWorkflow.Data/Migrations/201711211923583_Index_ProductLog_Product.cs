namespace ProductSellingWorkflow.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Index_ProductLog_Product : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ProductLogs", new[] { "ProductId" });
            CreateIndex("dbo.ProductLogs", "ProductId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProductLogs", new[] { "ProductId" });
            CreateIndex("dbo.ProductLogs", "ProductId");
        }
    }
}
