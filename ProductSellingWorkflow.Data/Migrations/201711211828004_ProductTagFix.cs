namespace ProductSellingWorkflow.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductTagFix : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ProductTags");
            AddPrimaryKey("dbo.ProductTags", new[] { "ProductId", "TagId" });
            DropColumn("dbo.ProductTags", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductTags", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.ProductTags");
            AddPrimaryKey("dbo.ProductTags", "Id");
        }
    }
}
