namespace Tutorias.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ver2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.StudentVulnerability", name: "StudenID", newName: "StudentID");
            RenameColumn(table: "dbo.StudentVulnerability", name: "VulneravilityID", newName: "VulnerabilityID");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.StudentVulnerability", name: "VulnerabilityID", newName: "VulneravilityID");
            RenameColumn(table: "dbo.StudentVulnerability", name: "StudentID", newName: "StudenID");
        }
    }
}
