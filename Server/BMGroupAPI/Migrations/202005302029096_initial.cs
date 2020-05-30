namespace BMGroupAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "Organization_OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.Employees", "Organization_OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.Ellectives", "Organization_OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.Classes", "Organization_OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.Lessons", "ClassId", "dbo.Classes");
            DropIndex("dbo.Lessons", new[] { "ClassId" });
            DropIndex("dbo.Events", new[] { "Organization_OrganizationId" });
            DropIndex("dbo.Employees", new[] { "Organization_OrganizationId" });
            DropIndex("dbo.Ellectives", new[] { "Organization_OrganizationId" });
            DropIndex("dbo.Classes", new[] { "Organization_OrganizationId" });
            DropTable("dbo.Organizations");
            DropTable("dbo.Lessons");
            DropTable("dbo.Events");
            DropTable("dbo.Employees");
            DropTable("dbo.Ellectives");
            DropTable("dbo.Classes");
        }
    }
}
