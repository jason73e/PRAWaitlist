namespace PRAWaitList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FamilyModel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FamilyName = c.String(nullable: false, maxLength: 50),
                        Address1 = c.String(nullable: false, maxLength: 150),
                        Address2 = c.String(maxLength: 50),
                        City = c.String(nullable: false, maxLength: 50),
                        StateID = c.String(nullable: false, maxLength: 3),
                        ZipCode = c.String(nullable: false, maxLength: 50),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        UpdateUserID = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HearAboutPRAModel",
                c => new
                    {
                        Value = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        IsChecked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Value);
            
            CreateTable(
                "dbo.ParentModel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FamilyID = c.Int(nullable: false),
                        pType = c.Int(nullable: false),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        EmailAddress = c.String(nullable: false),
                        Phone1 = c.String(nullable: false),
                        Phone1Type = c.Int(nullable: false),
                        Phone2 = c.String(nullable: false),
                        Phone2Type = c.Int(nullable: false),
                        Address1 = c.String(nullable: false),
                        Address2 = c.String(maxLength: 50),
                        City = c.String(nullable: false, maxLength: 50),
                        StateID = c.String(nullable: false, maxLength: 3),
                        ZipCode = c.String(nullable: false, maxLength: 50),
                        isPreferredContact = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        UpdateUserID = c.String(),
                        isActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SchoolModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SchoolName = c.String(nullable: false),
                        StateName = c.String(nullable: false),
                        StateAbbr = c.String(nullable: false),
                        SchoolID = c.String(nullable: false),
                        AgencyName = c.String(nullable: false),
                        AgencyID = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SchoolYearModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StartYear = c.Int(nullable: false),
                        EndYear = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SiblingModel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FamilyID = c.Int(nullable: false),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        BirthDate = c.DateTime(nullable: false),
                        isPRAStudent = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        UpdateUserID = c.String(),
                        isActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StateModel",
                c => new
                    {
                        StateID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.StateID);
            
            CreateTable(
                "dbo.StudentModel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FamilyID = c.Int(nullable: false),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        BirthDate = c.DateTime(nullable: false),
                        Gender = c.String(nullable: false),
                        CurrentGrade = c.Int(nullable: false),
                        ApplyGrade = c.Int(nullable: false),
                        ApplyYear = c.String(nullable: false),
                        LocalSchool = c.String(nullable: false),
                        LocalDistrict = c.String(nullable: false),
                        Status = c.String(),
                        LearnAboutPRA = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        UpdateUserID = c.String(),
                        isActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StudentModel");
            DropTable("dbo.StateModel");
            DropTable("dbo.SiblingModel");
            DropTable("dbo.SchoolYearModel");
            DropTable("dbo.SchoolModel");
            DropTable("dbo.ParentModel");
            DropTable("dbo.HearAboutPRAModel");
            DropTable("dbo.FamilyModel");
        }
    }
}
