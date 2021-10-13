namespace PRAWaitList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeRequiredextraphones : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ParentModel", "Phone2", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ParentModel", "Phone2", c => c.String(nullable: false));
        }
    }
}
