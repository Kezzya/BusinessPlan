namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newmigration21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WordTemplates", "DocumentConstructor_DocumentConstructorId", c => c.Int());
            AddColumn("dbo.DocumentConstructorCenterDatas", "DocumentConstructor_DocumentConstructorId", c => c.Int());
            AddColumn("dbo.DocumentConstructorLeftDatas", "DocumentConstructor_DocumentConstructorId", c => c.Int());
            CreateIndex("dbo.WordTemplates", "DocumentConstructor_DocumentConstructorId");
            CreateIndex("dbo.DocumentConstructorCenterDatas", "DocumentConstructor_DocumentConstructorId");
            CreateIndex("dbo.DocumentConstructorLeftDatas", "DocumentConstructor_DocumentConstructorId");
            AddForeignKey("dbo.DocumentConstructorLeftDatas", "DocumentConstructor_DocumentConstructorId", "dbo.DocumentConstructors", "DocumentConstructorId");
            AddForeignKey("dbo.DocumentConstructorCenterDatas", "DocumentConstructor_DocumentConstructorId", "dbo.DocumentConstructors", "DocumentConstructorId");
            AddForeignKey("dbo.WordTemplates", "DocumentConstructor_DocumentConstructorId", "dbo.DocumentConstructors", "DocumentConstructorId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WordTemplates", "DocumentConstructor_DocumentConstructorId", "dbo.DocumentConstructors");
            DropForeignKey("dbo.DocumentConstructorCenterDatas", "DocumentConstructor_DocumentConstructorId", "dbo.DocumentConstructors");
            DropForeignKey("dbo.DocumentConstructorLeftDatas", "DocumentConstructor_DocumentConstructorId", "dbo.DocumentConstructors");
            DropIndex("dbo.DocumentConstructorLeftDatas", new[] { "DocumentConstructor_DocumentConstructorId" });
            DropIndex("dbo.DocumentConstructorCenterDatas", new[] { "DocumentConstructor_DocumentConstructorId" });
            DropIndex("dbo.WordTemplates", new[] { "DocumentConstructor_DocumentConstructorId" });
            DropColumn("dbo.DocumentConstructorLeftDatas", "DocumentConstructor_DocumentConstructorId");
            DropColumn("dbo.DocumentConstructorCenterDatas", "DocumentConstructor_DocumentConstructorId");
            DropColumn("dbo.WordTemplates", "DocumentConstructor_DocumentConstructorId");
        }
    }
}
