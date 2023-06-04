namespace PRAWaitList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LotteryBatchupdate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LotteryBatchModel", "BatchGrade", c => c.Int(nullable: false));
            DropColumn("dbo.LotteryBatchModel", "Grade");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LotteryBatchModel", "Grade", c => c.Int(nullable: false));
            DropColumn("dbo.LotteryBatchModel", "BatchGrade");
        }
    }
}
