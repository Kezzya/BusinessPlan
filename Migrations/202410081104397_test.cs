namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountOutTypes",
                c => new
                    {
                        AccountOutTypeId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        WordTemplateId_WordTemplateId = c.Int(),
                    })
                .PrimaryKey(t => t.AccountOutTypeId)
                .ForeignKey("dbo.WordTemplates", t => t.WordTemplateId_WordTemplateId)
                .Index(t => t.WordTemplateId_WordTemplateId);
            
            CreateTable(
                "dbo.WordTemplates",
                c => new
                    {
                        WordTemplateId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.WordTemplateId);
            
            CreateTable(
                "dbo.PeriodTypes",
                c => new
                    {
                        PeriodTypeId = c.Int(nullable: false, identity: true),
                        WordTemplateId_WordTemplateId = c.Int(),
                    })
                .PrimaryKey(t => t.PeriodTypeId)
                .ForeignKey("dbo.WordTemplates", t => t.WordTemplateId_WordTemplateId)
                .Index(t => t.WordTemplateId_WordTemplateId);
            
            CreateTable(
                "dbo.Scenarios",
                c => new
                    {
                        ScenarioId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        WordTemplateId_WordTemplateId = c.Int(),
                    })
                .PrimaryKey(t => t.ScenarioId)
                .ForeignKey("dbo.WordTemplates", t => t.WordTemplateId_WordTemplateId)
                .Index(t => t.WordTemplateId_WordTemplateId);
            
            CreateTable(
                "dbo.DocumentConstructorCenterDatas",
                c => new
                    {
                        DocumentConstructorCenterDataId = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        DocumentConstructorLeftDataId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DocumentConstructorCenterDataId)
                .ForeignKey("dbo.DocumentConstructorLeftDatas", t => t.DocumentConstructorLeftDataId, cascadeDelete: true)
                .Index(t => t.DocumentConstructorLeftDataId);
            
            CreateTable(
                "dbo.DocumentConstructorLeftDatas",
                c => new
                    {
                        DocumentConstructorLeftDataId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Npp = c.Int(nullable: false),
                        SizeTitle = c.Int(),
                    })
                .PrimaryKey(t => t.DocumentConstructorLeftDataId);
            
            CreateTable(
                "dbo.DocumentConstructors",
                c => new
                    {
                        DocumentConstructorId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.DocumentConstructorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DocumentConstructorCenterDatas", "DocumentConstructorLeftDataId", "dbo.DocumentConstructorLeftDatas");
            DropForeignKey("dbo.Scenarios", "WordTemplateId_WordTemplateId", "dbo.WordTemplates");
            DropForeignKey("dbo.PeriodTypes", "WordTemplateId_WordTemplateId", "dbo.WordTemplates");
            DropForeignKey("dbo.AccountOutTypes", "WordTemplateId_WordTemplateId", "dbo.WordTemplates");
            DropIndex("dbo.DocumentConstructorCenterDatas", new[] { "DocumentConstructorLeftDataId" });
            DropIndex("dbo.Scenarios", new[] { "WordTemplateId_WordTemplateId" });
            DropIndex("dbo.PeriodTypes", new[] { "WordTemplateId_WordTemplateId" });
            DropIndex("dbo.AccountOutTypes", new[] { "WordTemplateId_WordTemplateId" });
            DropTable("dbo.DocumentConstructors");
            DropTable("dbo.DocumentConstructorLeftDatas");
            DropTable("dbo.DocumentConstructorCenterDatas");
            DropTable("dbo.Scenarios");
            DropTable("dbo.PeriodTypes");
            DropTable("dbo.WordTemplates");
            DropTable("dbo.AccountOutTypes");
        }
    }
}
