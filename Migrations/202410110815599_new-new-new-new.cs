namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newnewnewnew : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DocumentConstructors", "DocumentConstructorLeftDataId");
            DropColumn("dbo.DocumentConstructors", "DocumentConstructorCenterDataId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DocumentConstructors", "DocumentConstructorCenterDataId", c => c.Int(nullable: false));
            AddColumn("dbo.DocumentConstructors", "DocumentConstructorLeftDataId", c => c.Int(nullable: false));
        }
    }
}
