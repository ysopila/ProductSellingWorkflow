namespace ProductSellingWorkflow.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Size = c.String(),
                        Color = c.String(),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OperationId = c.Guid(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Operation = c.Int(nullable: false),
                        Property = c.String(),
                        Value = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductTags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductTags", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductTags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.ProductLogs", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductTags", new[] { "TagId" });
            DropIndex("dbo.ProductTags", new[] { "ProductId" });
            DropIndex("dbo.ProductLogs", new[] { "ProductId" });
            DropTable("dbo.Tags");
            DropTable("dbo.ProductTags");
            DropTable("dbo.ProductLogs");
            DropTable("dbo.Products");
        }
    }
}
