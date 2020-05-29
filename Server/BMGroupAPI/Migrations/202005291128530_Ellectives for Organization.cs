namespace BMGroupAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EllectivesforOrganization : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ellectives", "Organization_Id", c => c.Int());
            CreateIndex("dbo.Ellectives", "Organization_Id");
            AddForeignKey("dbo.Ellectives", "Organization_Id", "dbo.Organizations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ellectives", "Organization_Id", "dbo.Organizations");
            DropIndex("dbo.Ellectives", new[] { "Organization_Id" });
            DropColumn("dbo.Ellectives", "Organization_Id");
        }
    }
}
