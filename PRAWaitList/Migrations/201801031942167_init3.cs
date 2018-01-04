namespace PRAWaitList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PortalMenusModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MenuName = c.String(nullable: false),
                        ControllerName = c.String(nullable: false),
                        ActionName = c.String(nullable: false),
                        MenuType = c.String(nullable: false),
                        ParentID = c.Int(nullable: false),
                        Sortorder = c.Int(nullable: false),
                        RoleName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PortalMenusModel");
        }
    }
}
