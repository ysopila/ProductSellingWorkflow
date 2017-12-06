namespace ProductSellingWorkflow.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotifications : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserNotificationSettings",
                c => new
                    {
                        NotificationTypeId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        NotificationKinds = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.NotificationTypeId, t.UserId, t.RoleId })
                .ForeignKey("dbo.NotificationTypes", t => t.NotificationTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.NotificationTypeId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.NotificationTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Caption = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserNotificationSettings", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserNotificationSettings", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserNotificationSettings", "NotificationTypeId", "dbo.NotificationTypes");
            DropIndex("dbo.UserNotificationSettings", new[] { "RoleId" });
            DropIndex("dbo.UserNotificationSettings", new[] { "UserId" });
            DropIndex("dbo.UserNotificationSettings", new[] { "NotificationTypeId" });
            DropTable("dbo.NotificationTypes");
            DropTable("dbo.UserNotificationSettings");
        }
    }
}
