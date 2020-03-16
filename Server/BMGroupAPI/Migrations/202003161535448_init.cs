namespace BMGroupAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        ClassId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Organization_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ClassId)
                .ForeignKey("dbo.Organizations", t => t.Organization_Id)
                .Index(t => t.Organization_Id);
            
            CreateTable(
                "dbo.Ellectives",
                c => new
                    {
                        EllectiveId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Time = c.DateTime(nullable: false),
                        Info = c.String(),
                    })
                .PrimaryKey(t => t.EllectiveId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Photo = c.Binary(),
                        Info = c.String(),
                        Organization_Id = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Organizations", t => t.Organization_Id)
                .Index(t => t.Organization_Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Title = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        Photo = c.Binary(),
                        Organization_Id = c.Int(),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.Organizations", t => t.Organization_Id)
                .Index(t => t.Organization_Id);
            
            CreateTable(
                "dbo.Lessons",
                c => new
                    {
                        LessonId = c.Int(nullable: false, identity: true),
                        ClassId = c.Int(nullable: false),
                        Info = c.String(),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.LessonId);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "Organization_Id", "dbo.Organizations");
            DropForeignKey("dbo.Employees", "Organization_Id", "dbo.Organizations");
            DropForeignKey("dbo.Classes", "Organization_Id", "dbo.Organizations");
            DropIndex("dbo.Events", new[] { "Organization_Id" });
            DropIndex("dbo.Employees", new[] { "Organization_Id" });
            DropIndex("dbo.Classes", new[] { "Organization_Id" });
            DropTable("dbo.Organizations");
            DropTable("dbo.Lessons");
            DropTable("dbo.Events");
            DropTable("dbo.Employees");
            DropTable("dbo.Ellectives");
            DropTable("dbo.Classes");
        }
    }
}
