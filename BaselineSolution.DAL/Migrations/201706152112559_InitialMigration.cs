namespace BaselineSolution.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(),
                        Name = c.String(),
                        Description = c.String(),
                        CreationUserId = c.Int(),
                        CreationDate = c.DateTime(),
                        ModificationUserId = c.Int(),
                        ModificationDate = c.DateTime(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                        FirstName = c.String(),
                        Email = c.String(),
                        LoginCount = c.Int(),
                        LastLogin = c.DateTime(),
                        AccountId = c.Int(nullable: false),
                        CreationUserId = c.Int(),
                        CreationDate = c.DateTime(),
                        ModificationUserId = c.Int(),
                        ModificationDate = c.DateTime(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ParentId = c.Int(),
                        CreationUserId = c.Int(),
                        CreationDate = c.DateTime(),
                        ModificationUserId = c.Int(),
                        ModificationDate = c.DateTime(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.RoleRights",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        RightId = c.Int(nullable: false),
                        Allow = c.Boolean(),
                        CreationUserId = c.Int(),
                        CreationDate = c.DateTime(),
                        ModificationUserId = c.Int(),
                        ModificationDate = c.DateTime(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rights", t => t.RightId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.RightId);
            
            CreateTable(
                "dbo.Rights",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(),
                        Key = c.String(),
                        CreationUserId = c.Int(),
                        CreationDate = c.DateTime(),
                        ModificationUserId = c.Int(),
                        ModificationDate = c.DateTime(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rights", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Role_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Role_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Role_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "User_Id", "dbo.Users");
            DropForeignKey("dbo.RoleRights", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.RoleRights", "RightId", "dbo.Rights");
            DropForeignKey("dbo.Rights", "ParentId", "dbo.Rights");
            DropForeignKey("dbo.Roles", "ParentId", "dbo.Roles");
            DropForeignKey("dbo.Users", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Accounts", "ParentId", "dbo.Accounts");
            DropIndex("dbo.UserRoles", new[] { "Role_Id" });
            DropIndex("dbo.UserRoles", new[] { "User_Id" });
            DropIndex("dbo.Rights", new[] { "ParentId" });
            DropIndex("dbo.RoleRights", new[] { "RightId" });
            DropIndex("dbo.RoleRights", new[] { "RoleId" });
            DropIndex("dbo.Roles", new[] { "ParentId" });
            DropIndex("dbo.Users", new[] { "AccountId" });
            DropIndex("dbo.Accounts", new[] { "ParentId" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.Rights");
            DropTable("dbo.RoleRights");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Accounts");
        }
    }
}
