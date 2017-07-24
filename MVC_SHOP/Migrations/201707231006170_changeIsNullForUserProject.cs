namespace MVC_SHOP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeIsNullForUserProject : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.UserRoles");
            DropIndex("dbo.Users", new[] { "RoleId" });
            AlterColumn("dbo.Users", "RoleId", c => c.Int());
            AlterColumn("dbo.Users", "IsActive", c => c.Boolean());
            CreateIndex("dbo.Users", "RoleId");
            AddForeignKey("dbo.Users", "RoleId", "dbo.UserRoles", "RoleId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.UserRoles");
            DropIndex("dbo.Users", new[] { "RoleId" });
            AlterColumn("dbo.Users", "IsActive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Users", "RoleId", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "RoleId");
            AddForeignKey("dbo.Users", "RoleId", "dbo.UserRoles", "RoleId", cascadeDelete: true);
        }
    }
}
