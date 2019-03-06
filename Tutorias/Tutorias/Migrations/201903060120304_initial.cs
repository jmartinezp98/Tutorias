namespace Tutorias.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Career",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 80),
                        Building = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Registration = c.String(nullable: false, maxLength: 10),
                        FirstMidName = c.String(nullable: false, maxLength: 25),
                        LastNameP = c.String(nullable: false, maxLength: 20),
                        LastNameM = c.String(nullable: false, maxLength: 20),
                        BirthDate = c.String(nullable: false, maxLength: 20),
                        CURP = c.String(nullable: false, maxLength: 18),
                        RFC = c.String(nullable: false, maxLength: 14),
                        MaritalStatusID = c.Int(nullable: false),
                        PersonalPhone = c.String(maxLength: 15),
                        EmergencyPhone = c.String(nullable: false, maxLength: 15),
                        PersonalEmail = c.String(maxLength: 40),
                        AcademicEmail = c.String(nullable: false, maxLength: 30),
                        CareerID = c.Int(nullable: false),
                        ModalityID = c.Int(nullable: false),
                        TurnID = c.Int(nullable: false),
                        ClassGroupID = c.Int(nullable: false),
                        SituationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Career", t => t.CareerID, cascadeDelete: true)
                .ForeignKey("dbo.ClassGroup", t => t.ClassGroupID, cascadeDelete: true)
                .ForeignKey("dbo.MaritalStatus", t => t.MaritalStatusID, cascadeDelete: true)
                .ForeignKey("dbo.Modality", t => t.ModalityID, cascadeDelete: true)
                .ForeignKey("dbo.Situation", t => t.SituationID, cascadeDelete: true)
                .ForeignKey("dbo.Turn", t => t.TurnID, cascadeDelete: true)
                .Index(t => t.CareerID)
                .Index(t => t.ClassGroupID)
                .Index(t => t.MaritalStatusID)
                .Index(t => t.ModalityID)
                .Index(t => t.SituationID)
                .Index(t => t.TurnID);
            
            CreateTable(
                "dbo.ClassGroup",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GroupID = c.String(nullable: false, maxLength: 35),
                        Term = c.Int(nullable: false),
                        Section = c.String(nullable: false, maxLength: 2),
                        TutorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Tutor", t => t.TutorID, cascadeDelete: true)
                .Index(t => t.TutorID);
            
            CreateTable(
                "dbo.Tutor",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.String(nullable: false, maxLength: 8),
                        FirstMidName = c.String(nullable: false, maxLength: 25),
                        LastNameP = c.String(nullable: false, maxLength: 20),
                        LastNameM = c.String(nullable: false, maxLength: 20),
                        UserName = c.String(nullable: false, maxLength: 15),
                        UserPassword = c.String(nullable: false, maxLength: 15),
                        BirthDate = c.String(nullable: false, maxLength: 20),
                        CURP = c.String(nullable: false, maxLength: 18),
                        RFC = c.String(nullable: false, maxLength: 14),
                        MaritalStatusID = c.Int(nullable: false),
                        PersonalPhone = c.String(maxLength: 15),
                        EmergencyPhone = c.String(nullable: false, maxLength: 15),
                        PersonalEmail = c.String(maxLength: 30),
                        AcademicEmail = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.ID)
                //se cambio a false
                .ForeignKey("dbo.MaritalStatus", t => t.MaritalStatusID, cascadeDelete: false)
                .Index(t => t.MaritalStatusID);
            
            CreateTable(
                "dbo.MaritalStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 35),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Modality",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 35),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Situation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 35),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.StudentCourse",
                c => new
                    {
                        StudentID = c.Int(nullable: false),
                        CourseID = c.Int(nullable: false),
                        Unit = c.Int(nullable: false),
                        Grade = c.String(nullable: false, maxLength: 4),
                    })
                .PrimaryKey(t => new { t.StudentID, t.CourseID, t.Unit })
                .ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Student", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.CourseID)
                .Index(t => t.StudentID);
            
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 35),
                        Instructor = c.String(nullable: false, maxLength: 45),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.StudentVulnerability",
                c => new
                    {
                        StudenID = c.Int(nullable: false),
                        VulneravilityID = c.Int(nullable: false),
                        VulStatus = c.Int(nullable: false),
                        VulComments = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => new { t.StudenID, t.VulneravilityID })
                .ForeignKey("dbo.Student", t => t.StudenID, cascadeDelete: true)
                .ForeignKey("dbo.Vulnerability", t => t.VulneravilityID, cascadeDelete: true)
                .Index(t => t.StudenID)
                .Index(t => t.VulneravilityID);
            
            CreateTable(
                "dbo.Vulnerability",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 35),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Turn",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 35),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Student", "TurnID", "dbo.Turn");
            DropForeignKey("dbo.StudentVulnerability", "VulneravilityID", "dbo.Vulnerability");
            DropForeignKey("dbo.StudentVulnerability", "StudenID", "dbo.Student");
            DropForeignKey("dbo.StudentCourse", "StudentID", "dbo.Student");
            DropForeignKey("dbo.StudentCourse", "CourseID", "dbo.Course");
            DropForeignKey("dbo.Student", "SituationID", "dbo.Situation");
            DropForeignKey("dbo.Student", "ModalityID", "dbo.Modality");
            DropForeignKey("dbo.Student", "MaritalStatusID", "dbo.MaritalStatus");
            DropForeignKey("dbo.Student", "ClassGroupID", "dbo.ClassGroup");
            DropForeignKey("dbo.ClassGroup", "TutorID", "dbo.Tutor");
            DropForeignKey("dbo.Tutor", "MaritalStatusID", "dbo.MaritalStatus");
            DropForeignKey("dbo.Student", "CareerID", "dbo.Career");
            DropIndex("dbo.Student", new[] { "TurnID" });
            DropIndex("dbo.StudentVulnerability", new[] { "VulneravilityID" });
            DropIndex("dbo.StudentVulnerability", new[] { "StudenID" });
            DropIndex("dbo.StudentCourse", new[] { "StudentID" });
            DropIndex("dbo.StudentCourse", new[] { "CourseID" });
            DropIndex("dbo.Student", new[] { "SituationID" });
            DropIndex("dbo.Student", new[] { "ModalityID" });
            DropIndex("dbo.Student", new[] { "MaritalStatusID" });
            DropIndex("dbo.Student", new[] { "ClassGroupID" });
            DropIndex("dbo.ClassGroup", new[] { "TutorID" });
            DropIndex("dbo.Tutor", new[] { "MaritalStatusID" });
            DropIndex("dbo.Student", new[] { "CareerID" });
            DropTable("dbo.Turn");
            DropTable("dbo.Vulnerability");
            DropTable("dbo.StudentVulnerability");
            DropTable("dbo.Course");
            DropTable("dbo.StudentCourse");
            DropTable("dbo.Situation");
            DropTable("dbo.Modality");
            DropTable("dbo.MaritalStatus");
            DropTable("dbo.Tutor");
            DropTable("dbo.ClassGroup");
            DropTable("dbo.Student");
            DropTable("dbo.Career");
        }
    }
}
