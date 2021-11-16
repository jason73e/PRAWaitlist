namespace PRAWaitList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HearAboutPRAUpdate20211030 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HearAboutPRAModel", "bExtraText", c => c.Boolean(nullable: false));
            AddColumn("dbo.HearAboutPRAModel", "bOtherText", c => c.Boolean(nullable: false));
            AddColumn("dbo.HearAboutPRAModel", "iSortOrder", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HearAboutPRAModel", "iSortOrder");
            DropColumn("dbo.HearAboutPRAModel", "bOtherText");
            DropColumn("dbo.HearAboutPRAModel", "bExtraText");
        }
    }
}
