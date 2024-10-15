namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IhopeFinalMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DocumentConstructorCenterDatas", "ImageData", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DocumentConstructorCenterDatas", "ImageData");
        }
    }
}
