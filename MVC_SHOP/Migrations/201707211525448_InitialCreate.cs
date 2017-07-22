namespace MVC_SHOP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 255),
                        CreatedBy = c.String(maxLength: 255),
                        Creared = c.DateTime(),
                        ModifieddBy = c.String(maxLength: 255),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 255),
                        CreatedBy = c.String(maxLength: 255),
                        Creared = c.DateTime(),
                        ModifieddBy = c.String(maxLength: 255),
                        Modified = c.DateTime(),
                        Content = c.String(storeType: "ntext"),
                        Icon = c.String(),
                        Image = c.String(),
                        Catrgories_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Catrgories_Id)
                .Index(t => t.Catrgories_Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentsText = c.String(),
                        CreatedBy = c.String(maxLength: 255),
                        Creared = c.DateTime(),
                        ModifieddBy = c.String(maxLength: 255),
                        Modified = c.DateTime(),
                        Reply = c.String(),
                        News_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.News", t => t.News_Id)
                .Index(t => t.News_Id);
            
            CreateTable(
                "dbo.FeedBacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 255),
                        Email = c.String(),
                        Content = c.String(storeType: "ntext"),
                        PhoneNumber = c.String(),
                        CreatedBy = c.String(maxLength: 255),
                        Creared = c.DateTime(),
                        ModifieddBy = c.String(maxLength: 255),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Registers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false, maxLength: 255),
                        CreatedBy = c.String(),
                        Creared = c.DateTime(),
                        ModifieddBy = c.String(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Account = c.String(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Password = c.String(nullable: false),
                        ConfirmPassword = c.String(nullable: false),
                        Gender = c.String(),
                        Email = c.String(nullable: false),
                        PhoneNo = c.String(),
                        Address = c.String(),
                        DateOfBirth = c.DateTime(),
                        RoleId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        ImageUrl = c.String(),
                        Createdby = c.String(),
                        CreatedDate = c.DateTime(),
                        Modifiedby = c.String(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.UserRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.UserRoles");
            DropForeignKey("dbo.Comments", "News_Id", "dbo.News");
            DropForeignKey("dbo.News", "Catrgories_Id", "dbo.Categories");
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Comments", new[] { "News_Id" });
            DropIndex("dbo.News", new[] { "Catrgories_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Registers");
            DropTable("dbo.FeedBacks");
            DropTable("dbo.Comments");
            DropTable("dbo.News");
            DropTable("dbo.Categories");
        }
    }
}
