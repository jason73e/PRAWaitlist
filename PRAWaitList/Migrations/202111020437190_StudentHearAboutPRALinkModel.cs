namespace PRAWaitList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentHearAboutPRALinkModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentHearAboutPRALinkModel",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        HPRAId = c.Int(nullable: false),
                        StudentID = c.Int(nullable: false),
                        ReferralName = c.String(),
                        ReferralEmail = c.String(),
                        OtherText = c.String(),
                        StudentModel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StudentModel", t => t.StudentModel_Id)
                .Index(t => t.StudentModel_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentHearAboutPRALinkModel", "StudentModel_Id", "dbo.StudentModel");
            DropIndex("dbo.StudentHearAboutPRALinkModel", new[] { "StudentModel_Id" });
            DropTable("dbo.StudentHearAboutPRALinkModel");
        }
    }
}
