namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newnewnew : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DocumentConstructorLeftDatas", "DocumentConstructor_DocumentConstructorId", "dbo.DocumentConstructors");
            DropIndex("dbo.DocumentConstructorLeftDatas", new[] { "DocumentConstructor_DocumentConstructorId" });
            RenameColumn(table: "dbo.DocumentConstructorLeftDatas", name: "DocumentConstructor_DocumentConstructorId", newName: "DocumentConstructorId");
            AddColumn("dbo.DocumentConstructors", "DocumentConstructorLeftDataId", c => c.Int(nullable: false));
            AddColumn("dbo.DocumentConstructors", "DocumentConstructorCenterDataId", c => c.Int(nullable: false));
            AlterColumn("dbo.DocumentConstructorLeftDatas", "DocumentConstructorId", c => c.Int(nullable: false));
            CreateIndex("dbo.DocumentConstructorLeftDatas", "DocumentConstructorId");
            AddForeignKey("dbo.DocumentConstructorLeftDatas", "DocumentConstructorId", "dbo.DocumentConstructors", "DocumentConstructorId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DocumentConstructorLeftDatas", "DocumentConstructorId", "dbo.DocumentConstructors");
            DropIndex("dbo.DocumentConstructorLeftDatas", new[] { "DocumentConstructorId" });
            AlterColumn("dbo.DocumentConstructorLeftDatas", "DocumentConstructorId", c => c.Int());
            DropColumn("dbo.DocumentConstructors", "DocumentConstructorCenterDataId");
            DropColumn("dbo.DocumentConstructors", "DocumentConstructorLeftDataId");
            RenameColumn(table: "dbo.DocumentConstructorLeftDatas", name: "DocumentConstructorId", newName: "DocumentConstructor_DocumentConstructorId");
            CreateIndex("dbo.DocumentConstructorLeftDatas", "DocumentConstructor_DocumentConstructorId");
            AddForeignKey("dbo.DocumentConstructorLeftDatas", "DocumentConstructor_DocumentConstructorId", "dbo.DocumentConstructors", "DocumentConstructorId");
        }
    }
}
