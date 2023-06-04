namespace PRAWaitList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LotteryBatchupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LotteryBatchModel", "Grade", c => c.Int(nullable: false));
            DropColumn("dbo.LotteryBatchModel", "BatchType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LotteryBatchModel", "BatchType", c => c.Int(nullable: false));
            DropColumn("dbo.LotteryBatchModel", "Grade");
        }
    }
}
