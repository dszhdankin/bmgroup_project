namespace BMGroupAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        ClassId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Organization_OrganizationId = c.Int(),
                    })
                .PrimaryKey(t => t.ClassId)
                .ForeignKey("dbo.Organizations", t => t.Organization_OrganizationId, cascadeDelete: true)
                .Index(t => t.Organization_OrganizationId);
            
            CreateTable(
                "dbo.Ellectives",
                c => new
                    {
                        EllectiveId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Time = c.DateTime(nullable: false),
                        Info = c.String(),
                        Organization_OrganizationId = c.Int(),
                    })
                .PrimaryKey(t => t.EllectiveId)
                .ForeignKey("dbo.Organizations", t => t.Organization_OrganizationId, cascadeDelete: true)
                .Index(t => t.Organization_OrganizationId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Photo = c.Binary(),
                        Info = c.String(),
                        Organization_OrganizationId = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Organizations", t => t.Organization_OrganizationId, cascadeDelete: true)
                .Index(t => t.Organization_OrganizationId);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Title = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        Photo = c.Binary(),
                        Organization_OrganizationId = c.Int(),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.Organizations", t => t.Organization_OrganizationId, cascadeDelete: true)
                .Index(t => t.Organization_OrganizationId);
            
            CreateTable(
                "dbo.Lessons",
                c => new
                    {
                        LessonId = c.Int(nullable: false, identity: true),
                        ClassId = c.Int(nullable: false),
                        Info = c.String(),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.LessonId)
                .ForeignKey("dbo.Classes", t => t.ClassId, cascadeDelete: true)
                .Index(t => t.ClassId);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        OrganizationId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.OrganizationId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Events", "Organization_OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.Employees", "Organization_OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.Ellectives", "Organization_OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.Classes", "Organization_OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.Lessons", "ClassId", "dbo.Classes");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Lessons", new[] { "ClassId" });
            DropIndex("dbo.Events", new[] { "Organization_OrganizationId" });
            DropIndex("dbo.Employees", new[] { "Organization_OrganizationId" });
            DropIndex("dbo.Ellectives", new[] { "Organization_OrganizationId" });
            DropIndex("dbo.Classes", new[] { "Organization_OrganizationId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Organizations");
            DropTable("dbo.Lessons");
            DropTable("dbo.Events");
            DropTable("dbo.Employees");
            DropTable("dbo.Ellectives");
            DropTable("dbo.Classes");
        }
    }
}
