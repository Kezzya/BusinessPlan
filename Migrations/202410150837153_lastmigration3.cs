namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lastmigration3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WordTemplates", "DocumentConstructor_DocumentConstructorId", "dbo.DocumentConstructors");
            DropForeignKey("dbo.AccountOutTypes", "WordTemplateId_WordTemplateId", "dbo.WordTemplates");
            DropForeignKey("dbo.PeriodTypes", "WordTemplateId_WordTemplateId", "dbo.WordTemplates");
            DropForeignKey("dbo.Scenarios", "WordTemplateId_WordTemplateId", "dbo.WordTemplates");
            DropIndex("dbo.AccountOutTypes", new[] { "WordTemplateId_WordTemplateId" });
            DropIndex("dbo.WordTemplates", new[] { "DocumentConstructor_DocumentConstructorId" });
            DropIndex("dbo.PeriodTypes", new[] { "WordTemplateId_WordTemplateId" });
            DropIndex("dbo.Scenarios", new[] { "WordTemplateId_WordTemplateId" });
            DropTable("dbo.AccountOutTypes");
            DropTable("dbo.WordTemplates");
            DropTable("dbo.PeriodTypes");
            DropTable("dbo.Scenarios");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Scenarios",
                c => new
                    {
                        ScenarioId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        WordTemplateId_WordTemplateId = c.Int(),
                    })
                .PrimaryKey(t => t.ScenarioId);
            
            CreateTable(
                "dbo.PeriodTypes",
                c => new
                    {
                        PeriodTypeId = c.Int(nullable: false, identity: true),
                        WordTemplateId_WordTemplateId = c.Int(),
                    })
                .PrimaryKey(t => t.PeriodTypeId);
            
            CreateTable(
                "dbo.WordTemplates",
                c => new
                    {
                        WordTemplateId = c.Int(nullable: false, identity: true),
                        DocumentConstructor_DocumentConstructorId = c.Int(),
                    })
                .PrimaryKey(t => t.WordTemplateId);
            
            CreateTable(
                "dbo.AccountOutTypes",
                c => new
                    {
                        AccountOutTypeId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        WordTemplateId_WordTemplateId = c.Int(),
                    })
                .PrimaryKey(t => t.AccountOutTypeId);
            
            CreateIndex("dbo.Scenarios", "WordTemplateId_WordTemplateId");
            CreateIndex("dbo.PeriodTypes", "WordTemplateId_WordTemplateId");
            CreateIndex("dbo.WordTemplates", "DocumentConstructor_DocumentConstructorId");
            CreateIndex("dbo.AccountOutTypes", "WordTemplateId_WordTemplateId");
            AddForeignKey("dbo.Scenarios", "WordTemplateId_WordTemplateId", "dbo.WordTemplates", "WordTemplateId");
            AddForeignKey("dbo.PeriodTypes", "WordTemplateId_WordTemplateId", "dbo.WordTemplates", "WordTemplateId");
            AddForeignKey("dbo.AccountOutTypes", "WordTemplateId_WordTemplateId", "dbo.WordTemplates", "WordTemplateId");
            AddForeignKey("dbo.WordTemplates", "DocumentConstructor_DocumentConstructorId", "dbo.DocumentConstructors", "DocumentConstructorId");
        }
    }
}
