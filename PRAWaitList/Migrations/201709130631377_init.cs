namespace PRAWaitList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConfigurationSettingsModel",
                c => new
                    {
                        key = c.String(nullable: false, maxLength: 128),
                        value = c.String(),
                    })
                .PrimaryKey(t => t.key);
            
            CreateTable(
                "dbo.EmailControlModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FromAddress = c.String(),
                        SMTPDeliveryMethod = c.String(),
                        SMTPHost = c.String(),
                        SMTPPort = c.Int(nullable: false),
                        SMTPUser = c.String(),
                        SMTPPassword = c.String(),
                        SMTPEnableSSL = c.Boolean(nullable: false),
                        SMTPSendLimit = c.Int(nullable: false),
                        SMTPisActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EmailQueueModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        QueueDate = c.DateTime(nullable: false),
                        StatusModel = c.String(),
                        ErrorMessage = c.String(),
                        StatusDate = c.DateTime(nullable: false),
                        RecipientCount = c.Int(nullable: false),
                        MessageTo = c.String(),
                        MessageSubject = c.String(),
                        MessageCC = c.String(),
                        MessageBCC = c.String(),
                        MessageBody = c.String(),
                        MessageIsHtml = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
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
                "dbo.LotteryModel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LotteryBatchId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        RandomID = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(),
                        UpdateUserID = c.String(),
                        NotifyDate = c.DateTime(),
                        AcceptDate = c.DateTime(),
                        DeclineDate = c.DateTime(),
                        Notes = c.String(),
                        Status = c.String(),
                        ApplyYear = c.String(),
                        ApplyGrade = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LotteryBatchModel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BatchName = c.String(nullable: false),
                        SchoolYearID = c.Int(nullable: false),
                        BatchType = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        UpdateUserID = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.StatusModel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        UStudentID = c.String(),
                        UpdateUserID = c.String(),
                        isActive = c.Boolean(nullable: false),
                        isPRASibling = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StudentModel");
            DropTable("dbo.StatusModel");
            DropTable("dbo.StateModel");
            DropTable("dbo.SiblingModel");
            DropTable("dbo.SchoolYearModel");
            DropTable("dbo.SchoolModel");
            DropTable("dbo.ParentModel");
            DropTable("dbo.LotteryBatchModel");
            DropTable("dbo.LotteryModel");
            DropTable("dbo.HearAboutPRAModel");
            DropTable("dbo.FamilyModel");
            DropTable("dbo.EmailQueueModel");
            DropTable("dbo.EmailControlModel");
            DropTable("dbo.ConfigurationSettingsModel");
        }
    }
}
