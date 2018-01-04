namespace PRAWaitList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParentModel", "isStaff", c => c.Boolean(nullable: false));
            AddColumn("dbo.ParentModel", "isSAC", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ParentModel", "isSAC");
            DropColumn("dbo.ParentModel", "isStaff");
        }
    }
}
