namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newmigration : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Basements");
            DropTable("dbo.Headings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Headings",
                c => new
                    {
                        HeadingId = c.Int(nullable: false, identity: true),
                        HeadingTitle = c.String(),
                    })
                .PrimaryKey(t => t.HeadingId);
            
            CreateTable(
                "dbo.Basements",
                c => new
                    {
                        BasementId = c.Int(nullable: false, identity: true),
                        BasementTitle = c.String(),
                    })
                .PrimaryKey(t => t.BasementId);
            
        }
    }
}
