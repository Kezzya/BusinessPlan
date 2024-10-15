namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Basements",
                c => new
                    {
                        BasementId = c.Int(nullable: false, identity: true),
                        BasementTitle = c.String(),
                    })
                .PrimaryKey(t => t.BasementId);
            
            CreateTable(
                "dbo.Headings",
                c => new
                    {
                        HeadingId = c.Int(nullable: false, identity: true),
                        HeadingTitle = c.String(),
                    })
                .PrimaryKey(t => t.HeadingId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Headings");
            DropTable("dbo.Basements");
        }
    }
}
