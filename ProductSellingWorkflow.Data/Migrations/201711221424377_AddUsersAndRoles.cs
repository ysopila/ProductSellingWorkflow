namespace ProductSellingWorkflow.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUsersAndRoles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserInRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Products", "CreatedById", c => c.Int(nullable: false));
            AddColumn("dbo.ProductLogs", "CreatedById", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "CreatedById");
            CreateIndex("dbo.ProductLogs", "CreatedById");
            AddForeignKey("dbo.ProductLogs", "CreatedById", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Products", "CreatedById", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserInRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserInRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Products", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.ProductLogs", "CreatedById", "dbo.Users");
            DropIndex("dbo.UserInRoles", new[] { "RoleId" });
            DropIndex("dbo.UserInRoles", new[] { "UserId" });
            DropIndex("dbo.ProductLogs", new[] { "CreatedById" });
            DropIndex("dbo.Products", new[] { "CreatedById" });
            DropColumn("dbo.ProductLogs", "CreatedById");
            DropColumn("dbo.Products", "CreatedById");
            DropTable("dbo.Roles");
            DropTable("dbo.UserInRoles");
            DropTable("dbo.Users");
        }
    }
}
