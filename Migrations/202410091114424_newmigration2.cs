namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newmigration2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DocumentConstructors", "Header", c => c.String());
            AddColumn("dbo.DocumentConstructors", "Bottom", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DocumentConstructors", "Bottom");
            DropColumn("dbo.DocumentConstructors", "Header");
        }
    }
}
