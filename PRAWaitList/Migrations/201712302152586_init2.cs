namespace PRAWaitList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PRAMenuModel", "sortorder", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PRAMenuModel", "sortorder");
        }
    }
}
